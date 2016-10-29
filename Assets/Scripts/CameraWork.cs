using UnityEngine;
using System.Collections;

/// <summary>
/// Camera work. Follow a target
/// </summary>
public class CameraWork : MonoBehaviour
{

    public GameObject target;
    private Vector3 offset;
    private Vector3 HeightPos = new Vector3((float)0, (float)3, (float)0);

    // Use this for initialization
    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.transform.position + offset + HeightPos;
            transform.rotation = target.transform.rotation;
        }
    }
}