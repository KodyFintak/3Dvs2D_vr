using UnityEngine;
using System.Collections;

public class Benny_Castle_Gate_Opener : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("f")){
			gameObject.SetActive(false);
		}
	}
}
