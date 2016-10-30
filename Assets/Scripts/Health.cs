using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public int hitPoints = 100;
    int currentHitPoints;
	Master_Control skeleControl;
	Player playerScript;

	// Use this for initialization
	void Start () {
		if (this.name == "Dungeoneer(Clone)") {
			playerScript = GetComponent<Player> ();	
		} else {
			skeleControl = GetComponent<Master_Control> ();
		}
        currentHitPoints = hitPoints;
	}
	
    [PunRPC]
    void TakeDamage(int amt)
    {
        currentHitPoints -= amt;
		if (playerScript != null) {
			playerScript.setHealth (currentHitPoints);

		} else {
			skeleControl.setHealth (currentHitPoints);
		}
    }
}
