using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Master_Control : MonoBehaviour {

	Transform playerLocation;
	Animator animate;
	Follow follow;
	PathingFollow pathFollowScript;
	Draw_Path pathScript;
	NavMeshAgent agent;
	Skel_Control skeleControl;
	List<Transform> path;
	bool doneAttacking = false;
	bool firstTime = true;
	bool doItOnce = true;
	int health = 3;
	Combat combatScript;

	// Use this for initialization
	void Start () {
		animate = GetComponent<Animator> ();
		follow = GetComponent<Follow> ();
		pathFollowScript = GetComponent<PathingFollow> ();
		pathScript = GameObject.Find ("ForestPath").GetComponent<Draw_Path>();
		agent = GetComponent<NavMeshAgent> ();
		skeleControl = GetComponent<Skel_Control> ();
		playerLocation = GameObject.Find ("FirstPersonCharacter").transform;
		combatScript = GetComponent<Combat> ();
	}

//	public void setPath(List<Transform> path2Set){
//		path = path2Set;
//	}
	
	// Update is called once per frame
	void Update () {
		//check if receive damage 

		if (health <= 0) {
			skeleControl.setDeath (animate);
			StartCoroutine (SleepForDeath ());
		}
		if (Vector3.Distance (playerLocation.position, this.transform.position) < 25 || animate.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
			if (firstTime) {
				skeleControl.setRun (animate);
				firstTime = false;
			}
			if (!agent.isActiveAndEnabled) {
				agent.enabled = true;
			}
			follow.FollowThem (animate, agent, playerLocation, skeleControl,combatScript);
			doneAttacking = true;
		} else {
			if (agent.isActiveAndEnabled) {
				agent.Resume ();
				firstTime = true;
				if (doneAttacking) {
					if (doItOnce) {
						doItOnce = false;
						skeleControl.setIdle (animate);
						StartCoroutine (SleepForIdle ((animate.GetCurrentAnimatorStateInfo (0).length)));
					}
				} else {
					if (!animate.GetBool ("isIdle")) {
						pathFollowScript.Patrol (pathScript.actualPath, agent, animate, skeleControl);
					}
				}
			}
		}
	}

	public void MeleeDamage(int damage){
		health = health - damage;
	}

	void OnCollisionEnter(Collision col){
		health = health - 0;
	}

//	IEnumerator SleepForDam(){
//		yield return new WaitForSecondsRealtime(0.5f);
//		skeleControl.setRun (animate);
//	}

	IEnumerator SleepForIdle(float timeToWait){
		agent.enabled = false;
		yield return new WaitForSecondsRealtime(5f);
		skeleControl.setWalk (animate);
		agent.enabled = true;
		doneAttacking = false;
		doItOnce = true;
	}

	IEnumerator SleepForDeath(){
		agent.enabled = false;
		yield return new WaitForSecondsRealtime(4f);
		Destroy (gameObject);
	}
}