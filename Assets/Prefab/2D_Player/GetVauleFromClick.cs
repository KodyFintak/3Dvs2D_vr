using UnityEngine;
using System.Collections;

public class GetVauleFromClick : MonoBehaviour {
    public Vector3 distance;
    public Camera camera;
    void Start()
    {
        camera = GetComponent<Camera>();
    }
	void Update () {
        
        if (Input.GetButtonDown("Fire1"))
        {
            
            distance = transform.position;
            distance.y = 1;
           
        }
	}
    void OnDrawGizmosSelected(Vector3 distance)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(distance, new Vector3(2,2,2));
    }
}
