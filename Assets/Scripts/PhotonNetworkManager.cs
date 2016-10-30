using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Collections.Generic;

public class PhotonNetworkManager : MonoBehaviour {

	//public Camera standbyCamera;
	public static PhotonNetworkManager instance = null;
	SpawnSpot[] spawnSpots;

	public InputField room_name;
	//public InputField max_players;
	public GameObject roomPrefab;
	private List<GameObject> roomPrefabs = new List<GameObject>();

	void Awake()
	{
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject.transform);
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}

	void Start()
	{
		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot> ();
		PhotonNetwork.ConnectUsingSettings ("0.2.0");
	}


	public void ButtonEvents(string EVENT)
	{
		switch (EVENT) {
		case "CreateRoom":
			if (PhotonNetwork.JoinLobby ()) {

				RoomOptions RO = new RoomOptions ();
				RO.MaxPlayers = 4;
				//RO.MaxPlayers = byte.Parse (max_players.text);
				PhotonNetwork.CreateRoom (room_name.text,RO,TypedLobby.Default);

			}
			break;
		case "RefreshButton":
			if(PhotonNetwork.JoinLobby())
				RefreshRoomList();
			break;
		case "JoinRandomRoom":
			if(PhotonNetwork.JoinLobby())
				JoinRandomRoomButton ();
			break;
		case "Exit":
			Application.Quit ();
			break;
		}
	}

	void RefreshRoomList()
	{
		if (roomPrefabs.Count > 0) {
			for (int i = 0; i < roomPrefabs.Count; i++) {
				Destroy (roomPrefabs[i]);
			}

			roomPrefabs.Clear ();
		}

		for (int i = 0; i < PhotonNetwork.GetRoomList ().Length; i++) {
			//Debug.Log (PhotonNetwork.GetRoomList()[i].name);
			GameObject g = Instantiate(roomPrefab);
			g.transform.SetParent (roomPrefab.transform.parent);

			g.GetComponent<RectTransform> ().localScale = roomPrefab.GetComponent<RectTransform> ().localScale;
			g.GetComponent<RectTransform> ().localPosition = new Vector3(roomPrefab.GetComponent<RectTransform>().localPosition.x,roomPrefab.GetComponent<RectTransform>().localPosition.y - (i*50),roomPrefab.GetComponent<RectTransform>().localPosition.z);
			g.transform.FindChild("Room_Name_Text").GetComponent<Text>().text = PhotonNetwork.GetRoomList()[i].name;
			g.transform.FindChild ("Room_Info").GetComponent<Text> ().text = PhotonNetwork.GetRoomList () [i].playerCount + "/" + PhotonNetwork.GetRoomList () [i].maxPlayers;

			g.transform.FindChild ("Join").GetComponent<Button> ().onClick.AddListener (() => { JoinRoom(g.transform.FindChild("Room_Name_Text").GetComponent<Text>().text);});

			g.SetActive (true);
			roomPrefabs.Add (g);
		}
	}

	void JoinRandomRoomButton()
	{
		if (PhotonNetwork.GetRoomList ().Length > 0) {
			PhotonNetwork.JoinRandomRoom ();
		} else {
			Debug.LogError ("No Rooms");
		}
	}


	void JoinRoom(string roomName){

		bool available_room = false;

		foreach (RoomInfo RI in PhotonNetwork.GetRoomList()) {
			if (roomName == RI.name) {
				available_room = true;
				break;			
			} else {
				available_room = false;
			}
		}

		if (available_room)
			PhotonNetwork.JoinRoom (roomName);
		else
			Debug.Log ("No such room.");
	}

	void OnGUI()
	{
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
	}

	void OnJoinedLobby()
	{
		Debug.Log ("fail");
		Invoke("RefreshRoomList", 0.1f);
	}

	void OnPhotonJoinRoomFailed()
	{
		Debug.Log ("fail: ");
	}

	void OnJoinedRoom()
	{

		Debug.Log ("Room joined");

		if (spawnSpots == null) {
			Debug.LogError ("Nope.");
			return;
		}
		SpawnSpot mySpawnSpot = spawnSpots [Random.Range (0, spawnSpots.Length)];

		//standbyCamera.enabled = false;

		GameObject player = PhotonNetwork.Instantiate ("Dungeoneer", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
		//GameObject player = PhotonNetwork.Instantiate("Dungeoneer", Vector3.zero, Quaternion.identity, 0);

		//player.transform.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
		player.transform.GetComponent<CharacterController>().enabled = true;
		player.transform.FindChild ("FirstPersonCharacter").gameObject.SetActive (true);
	}

	void OnCreatedRoom()
	{
		Debug.Log ("Room created.");
	}

    //void OnPhotonPlayerConnected(PhotonPlayer newplayer)
    //{
    //    if (instantiatedAvatars == false PhotonNetwork.countOfPlayers == 2)
    //        {
    //}
}
