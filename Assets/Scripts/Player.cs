using UnityEngine;
using System.Collections;
using System;

public class Player : Photon.MonoBehaviour, IPunObservable
{

    [SerializeField]
    private Stat health;
    [SerializeField]
    private Stat mana;
    [SerializeField]
    private Stat key;

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    public SpellCast spell;
	public AudioClip hurt;
	public CameraWork cameraScript;
    private float fireSpellStart = 0f;
    private float fireSpellCooldown = 2f;
    public MeleeSystem melee;
	public AudioSource audioSource;
	public GameObject deathScreen;
    private float pickupRate = 0.5f;
    private float nextCheck;
   

    // ALLEN NG ADDED THESE TWO VARIABLES____________________________
    //private int maxHealth = 7;
    public int keyCount;
    public int maxKey = 4;
    //public GameObject keyObj;
    private int maxMana = 8;
	// ALLEN NG LALALALALLALALALAL___________________________________



	private Camera playerCamera;

    bool IsFiring;

    void Start()
    {
        if (!photonView.isMine)
        {
            return;
        }
        health.setBar(GameObject.Find("Healthbar").GetComponent<BarScript>());
        health.Initialize();
        mana.setBar(GameObject.Find("ManaBar").GetComponent<BarScript>());
        mana.Initialize();
        key.setBar(GameObject.Find("KeyBar").GetComponent<BarScript>());
        key.Initialize();

        audioSource = GetComponent<AudioSource> ();
		playerCamera = Camera.main;	
		cameraScript = playerCamera.GetComponent<CameraWork> ();
    }



    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine == false && PhotonNetwork.connected == true)
        {
            return;
        }

        if (photonView.isMine)
        {
            ProcessInputs();
        }

        int i = 0;
    }

    void Awake()
    {
        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
        if (photonView.isMine)
        {
            Player.LocalPlayerInstance = this.gameObject;
        }
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(this.gameObject);

    }


    public void setHealth(int newHealth)
    {
        if( newHealth < health.CurrentVal)
        {
            audioSource.PlayOneShot(hurt);
                        // hurt screen
        }
        health.CurrentVal = (float)newHealth;
        if (newHealth <= 0)
        {
            death();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (! photonView.isMine)
        {
            return;
        }
        if(Time.time <= nextCheck)
        {
            return;
        }

        nextCheck = Time.time + pickupRate;
        // -----------------------------------------ALLEN NG ADDED THESE _______________----------------________
        // Change the number in healthPotion or manaPotion to change the amount healed or gained per potion. 
        if (other.CompareTag ("Health")){
			healthPotion (4);
		}else if(other.CompareTag("Mana")){
			manaPotion (1);
		}
        // Don't know if this is okay, but it works for now :D
        //Gives Error that Tag is not Beam but the game still works.
        // _____________----------------------------- ALLEN NG ^^^^^^^__________________________________________
        if (other.CompareTag("Key"))
        {
            if (!PhotonNetwork.isMasterClient)
            {
                keyCount++;
                key.CurrentVal = key.CurrentVal + (float)1;
                if (keyCount == maxKey)
                {
                    GameObject.Find("2D_Player(Clone)").GetComponent<fuck2D>().GetComponent<PhotonView>().RPC("die2D", PhotonTargets.All);
                }
            }
        }

        // _____________----------------------------- ALLEN NG ^^^^^^^__________________________________________

    }
    void ProcessInputs()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            IsFiring = true;
            melee.swing();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (mana.CurrentVal != 0)
            {
                if (Time.time > fireSpellStart + fireSpellCooldown)
                {
                    spell.spellCast();
                    mana.CurrentVal -= 1;
                    fireSpellStart = Time.time;
                }
            }

        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(IsFiring);

        }
        else
        {
            // Network player, receive data
            this.IsFiring = (bool)stream.ReceiveNext();
        }
        if (IsFiring == true)
        {
            IsFiring = !IsFiring;
        }
    }
		
	void death(){

        PhotonNetwork.Destroy(gameObject);
        // ------------------------------------Allen Ng--------------------
        while (keyCount > 0)
        {
            PhotonNetwork.Instantiate("key_gold 1 1", transform.position, Quaternion.identity, 0);
            keyCount--;
        }
        // ---------------------------------------------------- ALLEN NG---

		GameObject deathTime = PhotonNetwork.Instantiate (deathScreen.name, new Vector3(this.transform.position.x, 15f, this.transform.position.z), Quaternion.Euler(90f,0f,0f), 0);
		if (playerCamera != null && cameraScript != null && photonView.isMine)
		{
			//playerCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
			cameraScript.target = deathTime;
		}
	}
    public void healthPotion(int change)
    {
        Debug.Log("Health Potion Function Called");
        Health h = GetComponent<Health>();
        if (h != null)
        {
            h.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.All, -change);
        }

    }

    public void manaPotion(int change)
    {
        Debug.Log("Mana Potion Function Called");
        mana.CurrentVal = mana.CurrentVal + change;
        if (mana.CurrentVal > maxMana)
        {
            mana.CurrentVal = maxMana;
        }
    }
    // --------------------------------------------------------------------
}
