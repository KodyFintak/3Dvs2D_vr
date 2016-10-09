using UnityEngine;
using System.Collections;

public class clickDragSpawn : MonoBehaviour {

	public Transform prefab ;
	public Transform enemy1;
	public Transform[] spawnPoints;

	private Transform spawn;
	private Rect rect = new Rect(0, 0, 100, 50);
	private Rect rect1 = new Rect(0, 50, 100, 50);
	private Rect rect2 = new Rect(0, 100, 100, 50);
	private Rect rect3 = new Rect(0, 150, 100, 50);
	private Rect rect4 = new Rect(0, 200, 100, 50);

	// Use this for initialization
	void Start () {
		
	}
	

	void  Update() {


		if (Input.GetMouseButton(0) && spawn != null) {
			var pos = Input.mousePosition;
			pos.z = -Camera.main.transform.position.z;
			spawn.transform.position = Camera.main.ScreenToWorldPoint(pos);
		}

		if (Input.GetMouseButtonUp(0)) {
			spawn = null;
		}
	}

	void OnGUI() {
		Event e = Event.current;

		if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition)) {
			var pos = Input.mousePosition;
			pos.z = -Camera.main.transform.position.z;
			pos = Camera.main.ScreenToWorldPoint(pos);
			spawn = Instantiate(prefab, pos, Quaternion.identity) as Transform;
		}


		if (e.type == EventType.MouseDown && rect1.Contains (e.mousePosition)) {
			Instantiate(enemy1, spawnPoints[0].position, spawnPoints[0].rotation);
		}

		if (e.type == EventType.MouseDown && rect2.Contains (e.mousePosition)) {
			Instantiate(enemy1, spawnPoints[1].position, spawnPoints[1].rotation);
		}

		if (e.type == EventType.MouseDown && rect3.Contains (e.mousePosition)) {
			Instantiate(enemy1, spawnPoints[2].position, spawnPoints[2].rotation);
		}

		if (e.type == EventType.MouseDown && rect4.Contains (e.mousePosition)) {
			Instantiate(enemy1, spawnPoints[3].position, spawnPoints[3].rotation);
		}
		GUI.Button (rect, "Spawn");
		GUI.Button (rect1, "Spot 1");
		GUI.Button (rect2, "Spot 2");
		GUI.Button (rect3, "Spot 3");
		GUI.Button (rect4, "Spot 4");
	}
}
