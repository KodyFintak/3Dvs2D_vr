using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cyclops_MasterControls : MonoBehaviour {

	private LayerMask detectionLayer;
	Collider[] hitColliders;
	private float checkRate;
	private float nextCheck;
	private float detectionRadius = 300;


	Transform myTransform;
	Vector3 playerLocation;
	Animator animate;
	Cyclops_Follow follow;
	NavMeshAgent agent;
	Cylops_AniControl cyclopControl;
	bool doneAttacking = false;
	bool firstTime = true;
	bool doItOnce = true;
	bool playerFound = false;
	int health = 10;
	Cyclops_Combat combatScript;
	Player playerScript;

	// Use this for initialization
	void Start () {
		SetInitialReferences();

	}
		
	// Update is called once per frame
	void Update () {
		//check if receive damage 
		if (health <= 0) {
			cyclopControl.setDeath (animate);
			StartCoroutine (SleepForDeath ());
		} 
		else {
			CheckIfPlayerInRange();

			if (playerFound == true && (Vector3.Distance (playerLocation, this.transform.position) < 400 || animate.GetCurrentAnimatorStateInfo (0).IsName ("Attack"))) {
				if (firstTime) {
					cyclopControl.setRun (animate);
					firstTime = false;
				}
				if (!agent.isActiveAndEnabled) {
					agent.enabled = true;
				}
				follow.FollowThem (animate, agent, playerLocation, cyclopControl, combatScript,playerScript);
				doneAttacking = true;
			} 
		}
	}

//	public void MeleeDamage(int damage){
//		health = health - damage;
//	}

	public void setHealth(int newHealth){
		health = newHealth;
	}

//	void OnCollisionEnter(Collision col){
//		health = health - 2;
//	}
		
	IEnumerator SleepForDeath(){
		//playerScript.addExp (1);
		// get rid of box collider soon please
		yield return new WaitForSecondsRealtime(3f);
		agent.enabled = false;
		PhotonNetwork.Destroy (gameObject);
	}

	void SetInitialReferences()
	{
		checkRate = Random.Range(0.8f, 1.2f);
		myTransform = transform;
		detectionLayer = 1 << 8;
		animate = GetComponent<Animator>();
		follow = GetComponent<Cyclops_Follow>();
		agent = GetComponent<NavMeshAgent>();
		cyclopControl = GetComponent<Cylops_AniControl>();
		playerScript = null;
		combatScript = GetComponent<Cyclops_Combat>();
		cyclopControl.setRun (animate);
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