using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections;

public class GameManager : Photon.PunBehaviour {
    [SerializeField]
    SpawnSpot[] spawnSpots;


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
        spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();

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
