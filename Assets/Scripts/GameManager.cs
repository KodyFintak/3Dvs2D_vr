using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections;

public class GameManager : Photon.PunBehaviour {
    [SerializeField]
    SpawnSpot[] spawnSpots;
    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;


    public void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
	
	// Update is called once per frame
	public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    void Start()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            if (Player.LocalPlayerInstance == null)
            {
                Debug.Log("We are Instantiating LocalPlayer from " + Application.loadedLevelName);
                spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
                if (spawnSpots == null)
                {
                    Debug.LogError("Nope.");
                    return;
                }
                SpawnSpot mySpawnSpot = spawnSpots[UnityEngine.Random.Range(0, spawnSpots.Length)];
                // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                PhotonNetwork.Instantiate(this.playerPrefab.name, mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
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
        Debug.Log("PhotonNetwork : Loading Level : 1");
        PhotonNetwork.LoadLevel("Level1");
    }

    #region Photon Messages

    public override void OnPhotonPlayerConnected(PhotonPlayer other)
    {
        Debug.Log("OnPhotonPlayerConnected() " + other.name); // not seen if you're the player connecting

        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected

            LoadArena();
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
