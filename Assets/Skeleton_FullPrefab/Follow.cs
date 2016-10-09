using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	bool attackSleep = true;
	bool hitting = true;

	// Function to chase player if in range 
	public void FollowThem (Animator animate, NavMeshAgent agent, Transform playerLocation, Skel_Control skeleControl,Combat combatScript,Player playerScript) {
		Vector3 direction = playerLocation.position - this.transform.position;
		bool isCurrentAttack = animate.GetCurrentAnimatorStateInfo (0).IsName ("Attack");
		if (Vector3.Distance (playerLocation.position, this.transform.position) < 25){
			direction.y = 0;
			if (!isCurrentAttack) { 
				this.transform.rotation = (Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f));
			}

			if (direction.magnitude > 5) {
				if (!isCurrentAttack) { 
					agent.Resume ();
					agent.SetDestination (playerLocation.transform.position);
					agent.speed = 7;
					agent.acceleration = 8;
				}
				skeleControl.setRun (animate);	// outside of loop to tell attack to stop
			} 

			else{
				skeleControl.setAttack (animate);
				if (hitting) {
					hitting = false;
					StartCoroutine (SleepForAttack());
					combatScript.Hit (playerLocation,playerScript);
				}
				agent.SetDestination (this.transform.position);
				agent.velocity = Vector3.zero;
				agent.Stop ();

				// then make it so he can turn at the end
				if (attackSleep){
					attackSleep = false;
					this.transform.rotation = (Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f));
					attackSleep = true;
				}
			}
		} 
	}

	IEnumerator SleepForAttack(){
		yield return new WaitForSecondsRealtime(2.7f);
		hitting = true;
	}
}
