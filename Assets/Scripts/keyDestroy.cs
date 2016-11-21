using UnityEngine;
using System.Collections;

public class keyDestroy : MonoBehaviour {

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Dungeoneer") {
			PhotonNetwork.Destroy (gameObject);
		}
	}
}
