using UnityEngine;
using System.Collections;

public class MeleeSystem : MonoBehaviour {

	int Damage = 1;
	public float Distance;
	float MaxDistance = 5;

	public void swing(){
		if(!gameObject.GetComponent<Animation> ().IsPlaying("Swing")){
			gameObject.GetComponent<Animation> ().Play ();
			RaycastHit hit;
			if (Physics.Raycast (transform.position, transform.TransformDirection(Vector3.forward),out hit)) {
				Distance = hit.distance;
				if (Distance < MaxDistance) {
					hit.collider.SendMessage ("MeleeDamage", Damage, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
}
