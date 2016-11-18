using UnityEngine;
using System.Collections;

public class SkelSpawn : MonoBehaviour {

	public GameObject skeleton;
	public Animator animate;
	public GameObject skeleClone;
	public float moveAhead;
	public Draw_Path pathScript;

	void Start() {
		moveAhead = skeleton.transform.position.x;
		animate = skeleton.GetComponent<Animator> ();
		pathScript = GameObject.Find ("Maze_Path").GetComponent<Draw_Path> ();
	}
	// Update is called once per frame

	void Update () {
		if (Input.GetKey (KeyCode.P)) {
			skeleClone = Instantiate (skeleton, new Vector3 (moveAhead,skeleton.transform.position.y, skeleton.transform.position.z), Quaternion.identity) as GameObject;
			moveAhead += 2;
		}
	}
}
