using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections;

public class GameManager : Photon.PunBehaviour {

    #region Public Variables
    [SerializeField]
    SpawnSpot[] spawnSpots;
    [Tooltip("The prefab to use for representing the dungeon master")]
    public GameObject dMPrefab;
    [Tooltip("The prefab to use for representing the dungeoneers")]
    public GameObject dungeoneerPrefab;

    #endregion

    #region Private Variables
    private int Playercount = 0;

    #endregion

    #region Public Methods

    public void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
	
	public void LeaveRoom()
    {
        Playercount -= 1;
        PhotonNetwork.LeaveRoom();
    }

    #endregion

    #region Private Methods
    void Start()
    {
        if (dungeoneerPrefab == null || dMPrefab == null )
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            if (Player.LocalPlayerInstance == null)
            {
                Debug.Log("We are Instantiating LocalPlayer from " + Application.loadedLevelName);
                GameObject player;
                if (PhotonNetwork.player.isMasterClient)
                {
                   player = PhotonNetwork.Instantiate(this.dMPrefab.name, new Vector3(77,100,0), Quaternion.Euler(90f,0f,0f), 0);
                    //CameraWork cameraScript = player.GetComponent<CameraWork>();
                    //if(cameraScript != null)
                    //{
                    //    cameraScript.target = player;
                    //}
                }
                else
                {
                    spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
                    if (spawnSpots == null)
                    {
                        Debug.LogError("Nope.");
                        return;
                    }
                    SpawnSpot mySpawnSpot = spawnSpots[UnityEngine.Random.Range(0, spawnSpots.Length)];
                    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                    player = PhotonNetwork.Instantiate(this.dungeoneerPrefab.name, mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
                }
                GameObject MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
                if (MainCamera != null)
                {
                    CameraWork cameraScript = MainCamera.GetComponent<CameraWork>();
                    if (cameraScript != null)
                    {
                        if(player.name == "2D_Player(Clone)")
                        {
                           MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y - 0.2f, MainCamera.transform.position.z);
                       }
                       cameraScript.target = player;
                    }
                }
                Playercount += 1;
            }
            else
            {
                Debug.Log("Ignoring scene load for " + Application.loadedLevelName);
            }
        }

    }

    void LoadArena()
    {
        if ( ! PhotonNetwork.isMasterClient )
        {
            Debug.LogError("PhotonNetwork: Trying to Load a level but we are not the master Client.");
        }
        else
        {
            Debug.Log("PhotonNetwork : Loading Level : 1");
            PhotonNetwork.LoadLevel("Level1");
        }
    }
    #endregion

    #region Photon Messages

    public override void OnPhotonPlayerConnected(PhotonPlayer other)
    {
        Debug.Log("OnPhotonPlayerConnected() " + other.name); // not seen if you're the player connecting

        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected
            //LoadArena();
        }
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
    {
        Debug.Log("OnPhotonPlayerDisconnected() " + other.name); // seen when other disconnects

        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected

            LoadArena();
        }
    }

    #endregion

}
