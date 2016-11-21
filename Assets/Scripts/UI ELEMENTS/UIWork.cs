using UnityEngine;
using System.Collections;

public class UIWork : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PhotonNetwork.isMasterClient) {
			GameObject.Destroy (gameObject);
		}
	}
}
