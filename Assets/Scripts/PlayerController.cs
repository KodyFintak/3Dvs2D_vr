using UnityEngine;
using System.Collections;

public class PlayerController : Photon.MonoBehaviour {
    FirstPersonController fpsScript;
	// Use this for initialization
	void Start () {
        fpsScript = GetComponent<FirstPersonController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (photonView.isMine)
        {
            fpsScript.movement();
        }
    }

    void FixedUpdate()
    {
        if (photonView.isMine)
        {
            fpsScript.FixedUpdateChange();
        }
    }
}
