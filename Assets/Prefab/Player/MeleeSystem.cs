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

			if (Physics.Raycast (transform.position, transform.TransformDirection(Vector3.forward),out hit,5f)) {
                if (hit.transform.CompareTag("Enemy") || hit.transform.CompareTag("Dungeoneer"))
                {
                    Health h = hit.transform.GetComponent<Health>();

                    if(h != null)
                    {
                        h.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.All, Damage);
                        //hit.collider.SendMessage("MeleeDamage", Damage, SendMessageOptions.DontRequireReceiver);
                    }
                }
			}
		}
	}
    
}
