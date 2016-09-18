using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [SerializeField]
    private Stat health;
    [SerializeField]
    private Stat mana;
    [SerializeField]
    private Stat xp;

    private void Awake()
    {
        health.Initialize();
        mana.Initialize();
        xp.Initialize();
    }
	
	// Update is called once per frame
	void Update ()
    {
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
    }
}
