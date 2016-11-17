using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PointScript : MonoBehaviour {

	public int howManyConnected;
	public bool straightLine;

	// Use this for initialization
	void Start () {
		howManyConnected = 1;
		straightLine = true;
	}

	public int Connected (){
		return howManyConnected;
	}
	public bool isItStraight(){
		return straightLine;
	}
}
