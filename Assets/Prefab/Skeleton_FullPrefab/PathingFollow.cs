using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathingFollow : MonoBehaviour {

	float roomToMove = 2f;
	int currentNode = 0; 
	bool waitForIdle = true;
	private bool _idleDone = false;

	public void Patrol(List<Transform> actualPath, NavMeshAgent agent, Animator animate,Skel_Control skeleControl) {
		agent.speed = 3;
		agent.acceleration = 3;
		Vector3 direction = actualPath[currentNode].position - this.transform.position;
		if (Vector3.Distance (actualPath[currentNode].position, this.transform.position) < roomToMove) {
			
			//if done with idle animation
			if (_idleDone) {
				currentNode++;
				waitForIdle = true; 
				_idleDone = false;
				if (currentNode >= actualPath.Count) {
					currentNode = 0;
					actualPath.Reverse ();
				} 
			}
			else {
				if (waitForIdle) {
					waitForIdle = false;
					skeleControl.setIdle (animate);
					StartCoroutine (SleepForIdle((animate.GetCurrentAnimatorStateInfo (0).length),skeleControl,animate));
				}
			}
		}
		else{
			skeleControl.setWalk (animate);
			this.transform.rotation = (Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f));
			agent.SetDestination (actualPath[currentNode].position);
		}
	}

	IEnumerator SleepForIdle(float timeToWait, Skel_Control skeleControl, Animator animate){
		yield return new WaitForSecondsRealtime(timeToWait);
		_idleDone = true;
		skeleControl.setWalk (animate);
	}
}
