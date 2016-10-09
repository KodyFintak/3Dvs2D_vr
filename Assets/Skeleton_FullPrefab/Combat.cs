using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {
	public float distance;

	public void Hit(Transform playerLocation){
		StartCoroutine (SleepForAttack (playerLocation));
	}
		
	IEnumerator SleepForAttack(Transform playerLocation){
		yield return new WaitForSecondsRealtime(1f);
		RaycastHit hit;
		if (Physics.Raycast (transform.position, playerLocation.position - transform.position,out hit)) {
			distance = hit.distance;
			if (hit.distance <= 5) {
				hit.collider.SendMessage ("AIMelee", 1, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
