using UnityEngine;
using System.Collections;

public class MeleeSystem : MonoBehaviour {

	int Damage = 1;
	public float Distance;
	public GameObject spells;
	float MaxDistance = 5;

	void Start(){
		spells = GameObject.Find ("Spells");
	}
	public void swing(){
		if(!gameObject.GetComponent<Animation> ().IsPlaying("Swing")){
			gameObject.GetComponent<Animation> ().Play ();
			RaycastHit hit;
			float distanceMove = (spells.transform.position.z - transform.position.z) * 1.0f;
			Vector3 lineFrom = new Vector3 (transform.position.x, transform.position.y, transform.position.z - distanceMove);
			if (Physics.Raycast (lineFrom, transform.TransformDirection(Vector3.forward),out hit,5f))
			{
                


				if (hit.transform.CompareTag("Enemy") || hit.transform.CompareTag("Dungeoneer"))
                {
                    Health h = hit.transform.GetComponent<Health>();



                    if(h != null)
                    {
                        h.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.All, Damage);
                    }
                }
			}
		}
	}
}
