using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour {

    [SerializeField]
    private Stat health;
    [SerializeField]
    private Stat mana;
    [SerializeField]
    private Stat xp;
	public SpellCast spell;
	private float fireSpellStart = 0f;
	private float fireSpellCooldown = 2f;
	public MeleeSystem melee;

    private void Awake()
    {
        health.Initialize();
        mana.Initialize();
        xp.Initialize();
    }
		


	// Update is called once per frame
	void Update ()
    {
		if (Input.GetButtonDown("Fire1")) {
			melee.swing ();
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			if (mana.CurrentVal != 0) {
				if (Time.time > fireSpellStart + fireSpellCooldown) {
					spell.spellCast ();
					mana.CurrentVal -= 1;
					fireSpellStart = Time.time;
				}
			}

		}
			
	    if (Input.GetKeyDown(KeyCode.Y))
        {
            health.CurrentVal -= 1;
        }
        if (Input.GetKeyUp(KeyCode.U))
        {
            health.CurrentVal += 1;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            mana.CurrentVal -= 1;
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            mana.CurrentVal += 1;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            xp.CurrentVal -= 1;
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            xp.CurrentVal += 1;
        }
		if (Input.GetKey (KeyCode.Escape)) {
			SceneManager.LoadScene ("Lobby");
		}
    }


}
