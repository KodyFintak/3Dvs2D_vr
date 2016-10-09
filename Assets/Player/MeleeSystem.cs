using UnityEngine;
using System.Collections;

public class MeleeSystem : MonoBehaviour {

	public int Damage = 10;
	public float Distance ;
	public float MaxDistance = 2;
	
	// Update is called once per frame
	void Update () {
		


	}

	public void swing(){
		if(!gameObject.GetComponent<Animation> ().IsPlaying("Swing")){
			gameObject.GetComponent<Animation> ().Play ();
			RaycastHit hit;
			if (Physics.Raycast (transform.position, transform.TransformDirection(Vector3.forward),out hit)) {
				Distance = hit.distance;
				if (Distance < MaxDistance) {
					hit.transform.SendMessage ("MeleeDamage", Damage, SendMessageOptions.DontRequireReceiver);
				}


			}
		}
	}
}
