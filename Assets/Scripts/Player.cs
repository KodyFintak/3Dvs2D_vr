using UnityEngine;
using System.Collections;
using System;

public class Player : Photon.MonoBehaviour, IPunObservable
{

    [SerializeField]
    private int health = 24;
    [SerializeField]
    private int mana = 8;
    [SerializeField]
    private int xp = 40;
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;
    public SpellCast spell;
    private float fireSpellStart = 0f;
    private float fireSpellCooldown = 2f;
    public MeleeSystem melee;
    bool IsFiring;

    void Start()
    {
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

    void OnTriggerEnter(Collider other)
    {
        if (! photonView.isMine)
        {
            return;
        }


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
}
