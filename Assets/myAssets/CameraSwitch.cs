using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {

	public Camera FirstPersonCharacter;
	public Camera CameraOne;
	public Camera CameraTwo;
	/*Camera Two;
	Camera Three;
	Camera Four;*/

	// Use this for initialization
	void Start () {
		FirstPersonCharacter.enabled = true;
		CameraOne.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("0")){
			CameraOne.enabled = false;
			CameraTwo.enabled = false;
			FirstPersonCharacter.enabled = true;
		}
		if(Input.GetKeyDown("1")){
			FirstPersonCharacter.enabled = false;
			CameraTwo.enabled = false;
			CameraOne.enabled= true;	
		}
		if (Input.GetKeyDown ("2")) {
			FirstPersonCharacter.enabled = false;
			CameraOne.enabled = false;
			CameraTwo.enabled = true;
		}
	}
}
