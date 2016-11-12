using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Master_Control : MonoBehaviour {

    private LayerMask detectionLayer;
    Collider[] hitColliders;
    private float checkRate;
    private float nextCheck;
    private float detectionRadius = 25;


	Transform myTransform;
    Vector3 playerLocation;
	Animator animate;
	Follow follow;
	PathingFollow pathFollowScript;
	clickDragSpawn pathScript;
	NavMeshAgent agent;
	Skel_Control skeleControl;
	List<Transform> path;
	bool doneAttacking = false;
	bool firstTime = true;
	bool doItOnce = true;
    bool playerFound = false;
	int health = 3;
	Combat combatScript;
	Player playerScript;

	// Use this for initialization
	void Start () {
        SetInitialReferences();

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
		else {
            CheckIfPlayerInRange();

			if (playerFound == true && (Vector3.Distance (playerLocation, this.transform.position) < 25 || animate.GetCurrentAnimatorStateInfo (0).IsName ("Attack"))) {
				if (firstTime) {
					skeleControl.setRun (animate);
					firstTime = false;
				}
				if (!agent.isActiveAndEnabled) {
					agent.enabled = true;
				}
				follow.FollowThem (animate, agent, playerLocation, skeleControl, combatScript,playerScript);
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
							Debug.Log (pathScript.actualPath[0]);
							pathFollowScript.Patrol (pathScript.actualPath[0], agent, animate, skeleControl);
						}
					}
				}
			}
		}
	}

	public void MeleeDamage(int damage){
		health = health - damage;
	}

	public void setHealth(int newHealth){
		health = newHealth;
	}

	void OnCollisionEnter(Collision col){
		health = health - 2;
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
		//playerScript.addExp (1);
		// get rid of box collider soon please
		yield return new WaitForSecondsRealtime(4f);
		agent.enabled = false;
		PhotonNetwork.Destroy (gameObject);
	}

    void SetInitialReferences()
    {
        checkRate = Random.Range(0.8f, 1.2f);
        myTransform = transform;
        detectionLayer = 1 << 8;

        animate = GetComponent<Animator>();
        follow = GetComponent<Follow>();
        pathFollowScript = GetComponent<PathingFollow>();
        string tempName = this.name.Substring(this.name.Length - 1);
		pathScript = GameObject.Find("2D_Player(Clone)").GetComponent<clickDragSpawn>();
        agent = GetComponent<NavMeshAgent>();
        skeleControl = GetComponent<Skel_Control>();
        //playerLocation = GameObject.Find ("FirstPersonCharacter").transform;
        //playerLocation = GameObject.Find("2D_Player(Clone)").transform;
        //playerScript = GameObject.Find ("Player").GetComponent<Player>();
        playerScript = null;
        combatScript = GetComponent<Combat>();
    }

	public int returnHealth(){
		return health;
	}
    void CheckIfPlayerInRange()
    {
        if (Time.time > nextCheck && agent == true)
        {
            nextCheck = Time.time + checkRate;

            hitColliders = Physics.OverlapSphere(myTransform.position, detectionRadius, detectionLayer);

            if (hitColliders.Length > 0)
            {
                agent.SetDestination(hitColliders[0].transform.position);
                playerLocation = hitColliders[0].transform.position;
                playerFound = true;
            }
            else
            {
                playerFound = false;
            }
        }
    }
}