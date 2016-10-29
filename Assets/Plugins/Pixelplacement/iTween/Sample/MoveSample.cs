using UnityEngine;
using System.Collections;

public class MoveSample : MonoBehaviour
{	
    public bool UpdatePosition = true;
    public float DelayUpdatePos = 1;
    public float FirstYPos = 5;
    public float MovementSpeed = 5;
	void Start(){
        UpdatePosition = true;
        DelayUpdatePos = 1;
        FirstYPos = 5;
        MovementSpeed = 5;
		iTween.MoveBy(gameObject, iTween.Hash("x", 2, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
	}
}

