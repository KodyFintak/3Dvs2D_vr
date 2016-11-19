using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public int hitPoints = 100;
    int currentHitPoints;
	Master_Control skeleControl;
	Player playerScript;
	Cyclops_MasterControls cyclopControl;

	// Use this for initialization
	void Start () {
		if (this.name == "Dungeoneer(Clone)") {
			playerScript = GetComponent<Player> ();	
		} 
		if(this.name == "Skeleton_FullPrefab(Clone)") {
			skeleControl = GetComponent<Master_Control> ();
		}
		if (this.name == "Cyclops_FullPrefab(Clone)") {
			cyclopControl = GetComponent<Cyclops_MasterControls> (); 
		}
        currentHitPoints = hitPoints;
	}
	
    [PunRPC]
    void TakeDamage(int amt)
    {
        currentHitPoints -= amt;
		if (playerScript != null) {
			playerScript.setHealth (currentHitPoints);

		} 
		if (skeleControl != null){
			skeleControl.setHealth (currentHitPoints);
		}
		if (cyclopControl != null) {
			cyclopControl.setHealth (currentHitPoints);
		}
    }
}
