using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

	// Use this for initialization
	SpawnSpot[] spawnSpots;
	void Start () {

		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot> ();

		if (spawnSpots == null) {
			Debug.LogError ("Nope.");
			return;
		}

		SpawnSpot mySpawnSpot = spawnSpots [Random.Range (0, spawnSpots.Length)];
		//standbyCamera.enabled = false;

		GameObject player = PhotonNetwork.Instantiate ("Dungeoneer", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);


		player.transform.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = true;
		player.GetComponent<CharacterController>().enabled = true;
		player.transform.FindChild ("FirstPersonCharacter").gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
