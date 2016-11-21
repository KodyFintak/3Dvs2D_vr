using UnityEngine;
using System.Collections;

public class manaPotPickUp : MonoBehaviour {


	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Dungeoneer"){
			PhotonNetwork.Destroy(gameObject);
		}
	}
}
