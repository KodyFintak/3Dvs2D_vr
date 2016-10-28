using UnityEngine;
using System.Collections;

public class Location : MonoBehaviour {
    public GameObject Terrain;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Terrain.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log(hit.point);
            }
        }
    }
}
