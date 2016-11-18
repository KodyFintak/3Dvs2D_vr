using UnityEngine;
using System.Collections;

public class ExplodeonContact : MonoBehaviour {

	public GameObject explosion; 
	
	void OnCollisionEnter(Collision col){
        Health h = col.transform.GetComponent<Health>();
        if (h != null)
        {
            h.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.All, 3);
            //hit.collider.SendMessage("MeleeDamage", Damage, SendMessageOptions.DontRequireReceiver);
        }
        SendMessage ("TakeMagicDamage", 2, SendMessageOptions.DontRequireReceiver);
		GameObject expl = PhotonNetwork.Instantiate("ErekiBall2", transform.position, Quaternion.identity,0) as GameObject;
		PhotonNetwork.Destroy (gameObject); 
		PhotonNetwork.Destroy (expl); 
	}

}
