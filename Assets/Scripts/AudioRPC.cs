using UnityEngine;
using System.Collections;

public class AudioRPC : MonoBehaviour {
    public AudioClip marco;
    public AudioClip polo;

    [PunRPC]
    void Marco()
    {
        Debug.Log("Marco");

        this.GetComponent<AudioSource>().clip = marco;
        this.GetComponent<AudioSource>().Play();

    }

    [PunRPC]
    void Polo()
    {
        this.GetComponent<AudioSource>().clip = polo;
        this.GetComponent<AudioSource>().Play();
    }
}
