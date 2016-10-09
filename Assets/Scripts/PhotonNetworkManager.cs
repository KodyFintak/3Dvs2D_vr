using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Collections.Generic;

public class PhotonNetworkManager : MonoBehaviour {

	public static PhotonNetworkManager instance = null;

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
		PhotonNetwork.ConnectUsingSettings ("0.1.0");
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
		Debug.Log ("Lobi");
		Invoke("RefreshRoomList", 0.1f);
	}

	void OnPhotonJoinRoomFailed()
	{
		Debug.Log ("Odaya giris yapilmadi: ");
	}

	void OnJoinedRoom()
	{
		Debug.Log ("Room joined");
		SceneManager.LoadScene (1);
	}

	void OnCreatedRoom()
	{
		Debug.Log ("Room created.");
	}
}
