using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {
	public float distance;

	public void Hit(Vector3 playerLocation,Player playerScript){
		StartCoroutine (SleepForAttack (playerLocation,playerScript));
	}
		
	IEnumerator SleepForAttack(Vector3 playerLocation,Player playerScript){
		yield return new WaitForSecondsRealtime(1f);
		if (Vector3.Distance (playerLocation, this.transform.position) <= 6) {
			//playerScript.AIMelee (1);
		}

//		if (Physics.Raycast (transform.position, playerLocation.transform.position - transform.position,out hit)) {
//			distance = hit.distance;
//			Debug.DrawLine (transform.position, hit.point, Color.red);
//			if (hit.distance <= 5) {
//				Debug.Log ("shit");
//				hit.transform.SendMessage ("AIMelee", 1, SendMessageOptions.DontRequireReceiver);
//			}
//		}

	}
}
