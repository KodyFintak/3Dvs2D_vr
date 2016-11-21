using UnityEngine;
using System.Collections;

public class fuck2D : Photon.MonoBehaviour
{

    public GameObject deathScreen;
    private Camera playerCamera;
    private CameraWork cameraScript;

    void Start()
    {
        playerCamera = Camera.main;
        cameraScript = playerCamera.GetComponent<CameraWork>();
    }

    // Use this for initialization

    [PunRPC]
    void die2D()
    {
        GetComponent<clickDragSpawn>().enabled = false;

    }

}