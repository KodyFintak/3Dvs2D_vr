using UnityEngine;
using System.Collections;

/// <summary>
/// Camera work. Follow a target
/// </summary>
public class CameraWork : MonoBehaviour
{

    public GameObject target;
    private Vector3 offset;
    private Vector3 HeightPos = new Vector3(0f, 0.7f, 0f);

    // Update is called once per frame
    void LateUpdate()
    {	
		if (target != null) {
			transform.position = target.transform.position + HeightPos;
			transform.rotation = target.transform.rotation;
		}
    }
}