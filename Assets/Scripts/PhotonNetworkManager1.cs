using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

public class PhotonNetworkManager1 : Photon.PunBehaviour
{

    #region Global Variables

    public static PhotonNetworkManager1 instance = null;

    #endregion

    #region Public Variables

    public InputField room_name;
    public GameObject roomPrefab;
    public PhotonLogLevel LogLevel = PhotonLogLevel.Informational;
    [SerializeField]
    Text connectionText;
    [SerializeField]
    Transform[] spawnPoints;

    #endregion

    #region Private Variables

    private List<GameObject> roomPrefabs = new List<GameObject>();
    GameObject player;
    string _gameVersion = "0.2.0";

    #endregion

    #region Photon.PunBehaviour Callbacks

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject.transform);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        PhotonNetwork.logLevel = LogLevel;
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = true;
    }

    void Start()
    {
        Debug.Log(PhotonNetwork.countOfPlayers);
    }

    void RefreshRoomList()
    {
        if (roomPrefabs.Count > 0)
        {
            for (int i = 0; i < roomPrefabs.Count; i++)
            {
                Destroy(roomPrefabs[i]);
            }

            roomPrefabs.Clear();
        }

        for (int i = 0; i < PhotonNetwork.GetRoomList().Length; i++)
        {
            GameObject g = Instantiate(roomPrefab);
            g.transform.SetParent(roomPrefab.transform.parent);

            g.GetComponent<RectTransform>().localScale = roomPrefab.GetComponent<RectTransform>().localScale;
            g.GetComponent<RectTransform>().localPosition = new Vector3(roomPrefab.GetComponent<RectTransform>().localPosition.x, roomPrefab.GetComponent<RectTransform>().localPosition.y - (i * 50), roomPrefab.GetComponent<RectTransform>().localPosition.z);
            g.transform.FindChild("Room_Name_Text").GetComponent<Text>().text = PhotonNetwork.GetRoomList()[i].name;
            g.transform.FindChild("Room_Info").GetComponent<Text>().text = PhotonNetwork.GetRoomList()[i].playerCount + "/" + PhotonNetwork.GetRoomList()[i].maxPlayers;

            g.transform.FindChild("Join").GetComponent<Button>().onClick.AddListener(() => { JoinRoom(g.transform.FindChild("Room_Name_Text").GetComponent<Text>().text); });

            g.SetActive(true);
            roomPrefabs.Add(g);
        }
    }

    void JoinRandomRoomButton()
    {
        if (PhotonNetwork.GetRoomList().Length > 0)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            Debug.LogError("No Rooms");
        }
    }


    void JoinRoom(string roomName)
    {

        bool available_room = false;

        foreach (RoomInfo RI in PhotonNetwork.GetRoomList())
        {
            if (roomName == RI.name)
            {
                available_room = true;
                break;
            }
            else
            {
                available_room = false;
            }
        }

        if (available_room)
            PhotonNetwork.JoinRoom(roomName);
        else
            Debug.Log("No such room.");
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnPhotonJoinRoomFailed()
    {
        Debug.Log("fail: ");
    }

    void OnJoinedRoom()
    {

        Debug.Log("Room joined");
        if (PhotonNetwork.room.playerCount == 1)
        {
            Debug.Log("We load the 'Room for 1' ");
            PhotonNetwork.LoadLevel("Level2");
        }
    }

    void OnCreatedRoom()
    {
        Debug.Log("Room created.");
    }

    #endregion

    #region Public Methods

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings(_gameVersion);
    }

    public void ButtonEvents(string EVENT)
    {
        switch (EVENT)
        {
            case "CreateRoom":
                if (PhotonNetwork.JoinLobby())
                {

                    RoomOptions RO = new RoomOptions();
                    RO.MaxPlayers = 4;
                    PhotonNetwork.CreateRoom(room_name.text, RO, TypedLobby.Default);

                }
                break;
            case "RefreshButton":
                if (PhotonNetwork.JoinLobby())
                    RefreshRoomList();
                break;
            case "JoinRandomRoom":
                if (PhotonNetwork.JoinLobby())
                    JoinRandomRoomButton();
                break;
            case "Exit":
                Application.Quit();
                break;
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("fail");
        Invoke("RefreshRoomList", 0.1f);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Launcher: OnConnectedToMaster() was called by PUN");
    }

    public override void OnDisconnectedFromPhoton()
    {
        Debug.LogWarning("Launcher: OnDisconnectedFromPhoton() was called by PUN.");
    }
    #endregion
}

