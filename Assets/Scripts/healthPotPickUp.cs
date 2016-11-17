using UnityEngine;
using System.Collections;

public class healthPotPickUp : Photon.MonoBehaviour {


	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Dungeoneer"){
			PhotonView.Destroy (gameObject);
		}
	}
}
