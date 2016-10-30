using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class clickDragSpawn : Photon.MonoBehaviour {

	public GameObject enemy1;
	public Transform[] spawnPoints;
    public RaycastHit hit;
    public Ray ray;
    public Vector3 distance;
    public List<GameObject> path = new List<GameObject>();
    public GameObject op;
    public GameObject key;

    public GameObject tempNode;
    public int pathCounter;
    public int keyCounter;

    Transform spawn;
	Rect rect = new Rect(0, 0, 125, 50);
	Rect rect1 = new Rect(0, 50, 125, 50);
	Rect rect2 = new Rect(0, 100, 125, 50);
	Rect rect3 = new Rect(0, 150, 125, 50);
	Rect rect4 = new Rect(0, 200, 125, 50);
    Rect rect5 = new Rect(0, 250, 125, 50);
    Camera camera;

    // Use this for initialization
    void Start () {
        pathCounter = 0;
        keyCounter = 0;
        enemy1 = Resources.Load("Skeleton_FullPrefab") as GameObject;
        camera = Camera.main;
	}
	void Update() {
        if (photonView.isMine)
        {
            if (Input.GetMouseButton(0) && spawn != null)
            {
                var pos = Input.mousePosition;
                pos.z = -Camera.main.transform.position.z;
                spawn.transform.position = Camera.main.ScreenToWorldPoint(pos);
            }


            if (Input.GetMouseButtonUp(0))
            {
                spawn = null;
            }
        }
    }

	void OnGUI() {
        if (photonView.isMine)
        {
            Event e = Event.current;

            if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
            {
                path.Add(Instantiate(op));
                path[pathCounter].gameObject.name = "Path" + pathCounter;
                StartCoroutine(StartPath());
            }


            if (e.type == EventType.MouseDown && rect1.Contains(e.mousePosition))
            {
                PhotonNetwork.Instantiate(enemy1.name, spawnPoints[0].position, spawnPoints[0].rotation, 0);
            }

            if (e.type == EventType.MouseDown && rect2.Contains(e.mousePosition))
            {
                PhotonNetwork.Instantiate(enemy1.name, spawnPoints[1].position, spawnPoints[1].rotation, 0);
            }

            if (e.type == EventType.MouseDown && rect3.Contains(e.mousePosition))
            {
                PhotonNetwork.Instantiate(enemy1.name, spawnPoints[2].position, spawnPoints[2].rotation, 0);
            }

            if (e.type == EventType.MouseDown && rect4.Contains(e.mousePosition))
            {
                PhotonNetwork.Instantiate(enemy1.name, spawnPoints[3].position, spawnPoints[3].rotation, 0);
            }
            if (e.type == EventType.MouseDown && rect5.Contains(e.mousePosition))
            {
                StartCoroutine(spawnKey());
            }
            GUI.Button(rect, "Spawn");
            GUI.Button(rect1, "Spot 1");
            GUI.Button(rect2, "Spot 2");
            GUI.Button(rect3, "Spot 3");
            GUI.Button(rect4, "Spot 4");
            GUI.Button(rect5, (4 - keyCounter) + " Keys Remaining");
        }
	}

    IEnumerator StartPath()
    {
        do
        {
            yield return null;
        } while (!Input.GetMouseButtonUp(0));
        ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        Debug.Log(Physics.Raycast(ray,out hit));
        if (Physics.Raycast(ray, out hit) && hit.transform.name == "Terrain")
        {
            distance = hit.point;
            distance.y = 1;
            Debug.Log(tempNode.name);
            (PhotonNetwork.Instantiate(tempNode.name, distance, Quaternion.identity,0) as GameObject).transform.parent = path[pathCounter].transform;
        }
        //UnityEditor.Selection.activeGameObject = path[pathCounter];
        path[pathCounter].gameObject.SetActive(true); 
        StartCoroutine(ContinuePath());
    }

    IEnumerator ContinuePath()
    {
        while (!Input.GetKeyDown("return"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit) && hit.transform.name == "Terrain")
                {
                    distance = hit.point;
                    distance.y = 1;
                    (PhotonNetwork.Instantiate(tempNode.name, distance, Quaternion.identity,0)as GameObject).transform.parent = path[pathCounter].transform;
                }
            }
            yield return null;
        }
        (PhotonNetwork.Instantiate(enemy1.name, path[pathCounter].transform.GetChild(0).position, Quaternion.identity,0) as GameObject).gameObject.name = "Skeleton" + pathCounter;
        //UnityEditor.Selection.activeGameObject = null;
        //gameObject.SetActive(false);
        rect.position = new Vector2(-200, 0);
        Invoke("coolDown", 3.0f);        
        
        pathCounter++;
    }

    void coolDown()
    {
        rect.position = new Vector2(0, 0);
    }

    IEnumerator spawnKey()
    {
        do
        {
            yield return null;
        } while (!Input.GetMouseButtonUp(0));
        if(keyCounter < 4) { 
        ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.transform.name == "Terrain")
            {
                distance = hit.point;
                distance.y = 1;
                PhotonNetwork.Instantiate(key.name, distance, Quaternion.identity,0);
                keyCounter++;
            }
        }
    }
}