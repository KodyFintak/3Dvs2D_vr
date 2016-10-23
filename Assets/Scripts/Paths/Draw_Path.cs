using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Draw_Path : MonoBehaviour {

	public Color lineColor = Color.red;
	public List<Transform> actualPath = new List<Transform> ();
	public List<List<Transform>> adjanceyList = new List<List<Transform>>(); // need to make this adjancy list rep of a graph
	Transform[] pathArray;

//	public void setPath(){
//		int j = 0;
//		foreach (Transform i in actualPath) {
//			adjanceyList.Add (new List<Transform>());
//			adjanceyList [j].Add (i);
//			j++;
//		}
//
//	}

	void OnDrawGizmos(){
		Gizmos.color = lineColor;
		pathArray = this.GetComponentsInChildren<Transform> ();
		actualPath.Clear ();
		int name = 0;
		foreach (Transform element in pathArray) {
			if (element != this.transform) {
				element.name = name.ToString();
				name++;
				actualPath.Add (element);
			}
		}
			
		for (int i = 0; i < actualPath.Count; i++) {
			Vector3 currentPostion = actualPath [i].position;
			if (i > 0) {
				Vector3 previousPostion = actualPath [i - 1].position;
				Gizmos.DrawLine (previousPostion, currentPostion);	// allow the use of drawing lines between any two nodes 
				Gizmos.DrawWireSphere (currentPostion, 0.5f);
			}
		}
	}
}
