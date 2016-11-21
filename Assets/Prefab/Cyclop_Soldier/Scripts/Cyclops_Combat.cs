using UnityEngine;
using System.Collections;

public class Cyclops_Combat : MonoBehaviour {
	public float distance;
	private Cyclops_MasterControls masterControlScript;

	void Start(){
		masterControlScript = GetComponent<Cyclops_MasterControls> ();
	}
	public void Hit(Vector3 playerLocation,Player playerScript){
		StartCoroutine (SleepForAttack (playerLocation,playerScript));
	}

	IEnumerator SleepForAttack(Vector3 playerLocation,Player playerScript){
		yield return new WaitForSecondsRealtime(1f);
		if (masterControlScript.returnHealth () > 0) {
			RaycastHit hit; 
			if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hit)) {
				distance = hit.distance;
				Debug.DrawLine (transform.position, hit.point, Color.red);
				if (hit.distance <= 5) {
					if (hit.transform.CompareTag("Dungeoneer"))
					{
						Health h = hit.transform.GetComponent<Health>();

						if(h != null && PhotonNetwork.isMasterClient)
						{
							h.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.All, 3);
							//hit.collider.SendMessage("MeleeDamage", Damage, SendMessageOptions.DontRequireReceiver);
						}
					}
				}
			}
		}
	}
}
