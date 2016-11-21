using UnityEngine;
using System.Collections;

public class healthPotPickUp : MonoBehaviour {


	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Dungeoneer"){
			PhotonNetwork.Destroy (gameObject);
		}
	}
}
