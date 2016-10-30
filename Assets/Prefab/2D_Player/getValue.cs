using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class getValue : MonoBehaviour
{

    public RaycastHit hit;
    public Ray ray;
    public Vector3 distance;
    public List<GameObject> path = new List<GameObject>();
    public GameObject tempNode;
    public GameObject pathObject;

    void Start()
    {
        pathObject = GameObject.Find("Path");
    }
    void MakePath()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.transform.name == "Terrain")
            {
                distance = hit.point;
                distance.y = 1;
                GameObject toAddNode = Instantiate(tempNode, distance, Quaternion.identity) as GameObject;
                path.Add(toAddNode);
                toAddNode.transform.parent = pathObject.transform;
            }
        }
    }
}
