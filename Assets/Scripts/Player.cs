using UnityEngine;
using System.Collections;
using System;

public class Player : Photon.MonoBehaviour, IPunObservable
{

    [SerializeField]
    private int health = 7;
    [SerializeField]
    private int mana = 8;
    [SerializeField]
    private int xp = 40;
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;
    public SpellCast spell;
	public AudioClip hurt;
	public CameraWork cameraScript;
    private float fireSpellStart = 0f;
    private float fireSpellCooldown = 2f;
    public MeleeSystem melee;
	public AudioSource audioSource;

	// ALLEN NG ADDED THESE 5 VARIABLES AND A RECTANGLEEEEEE________
	private int maxHealth = 7;
	private int maxMana = 8;
	public int keyCount;
	public int maxKey = 4;
	Rect rect = new Rect (Screen.width - 125, 15, 125, 25);
	public GameObject key;
	// ALLEN NG LALALALALLALALALAL-_-_-_-_-_-_-_-_-_________________

	private Camera playerCamera;

    bool IsFiring;

    void Start()
    {
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

    public void AIMelee(int damage)
    {
        health = health - damage;
    }

    public void addExp(int expPoint)
    {
        xp = xp + expPoint;
    }

	public void setHealth(int newHealth){
		health = newHealth;
		if (health <= 0) {
			death ();	
		} else {
			audioSource.PlayOneShot(hurt);
			// hurt screen
		}
	}

    void OnTriggerEnter(Collider other)
    {
        if (! photonView.isMine)
        {
            return;
        }


		// -----------------------------------------ALLEN NG ADDED THESE _______________----------------________
		// Change the number in healthPotion or manaPotion to change the amount healed or gained per potion. 
		if (other.CompareTag ("Health")) {
			healthPotion (1);
		}else if(other.CompareTag("Mana")){
			manaPotion (1);
		}
		// Don't know if this is okay, but it works for now :D
		//Gives Error that Tag is not Beam but the game still works.

		if (other.CompareTag ("Key")) {
			keyCount++;
			if (keyCount == maxKey) {
				Debug.Log ("All keys are collected!, 2D player is dead :(");
				// INPUT GAME OVER SCREEN FOR 2D PLAYER HERERERERRERERRRRER!
			}
		}

		// _____________----------------------------- ALLEN NG ^^^^^^^__________________________________________

        // rule out any trigger not tagged "Beam". We are only interested in Beamers
		if (!other.CompareTag("Beam"))
        {
            return;
        }

        health -= 1;
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
            if (mana != 0)
            {
                if (Time.time > fireSpellStart + fireSpellCooldown)
                {
                    spell.spellCast();
                    mana -= 1;
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
		// ------------------------------------Allen Ng--------------------
		if (keyCount > 0) {
			PhotonNetwork.Instantiate ("key_gold 1 1", transform.position, Quaternion.identity, 0);
			keyCount--;
		}
		// ---------------------------------------------------- ALLEN NG---

		PhotonView.Destroy (gameObject);
		GameManager game = GameObject.Find ("GameManager").GetComponent<GameManager>();
		game.LeaveRoom ();
	}


	// --------------------------- ALLEN NG CODE -------------------------- 

	public void healthPotion(int change){
		Debug.Log ("Health Potion Function Called");
		health = health + change;
		if(health > maxHealth){
			health = maxHealth;
		}
	}

	public void manaPotion (int change){
		Debug.Log ("Mana Potion Function Called");
		mana = mana + change;
		if(mana > maxMana){
			mana = maxMana;
		}
	}

	void OnGUI(){
		if (photonView.isMine) {
			GUI.Button (rect, keyCount + (" Keys Collected"));
		}
	}
	// --------------------------------------------------------------------
}
