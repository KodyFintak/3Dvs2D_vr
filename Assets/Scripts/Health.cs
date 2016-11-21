using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public int hitPoints = 100;
    public int currentHitPoints;
	Master_Control skeleControl;
	Player playerScript;
	Cyclops_MasterControls cyclopControl;
    int maxHealth;

	// Use this for initialization
	void Start () {
		if (this.name == "Dungeoneer(Clone)") {
			playerScript = GetComponent<Player> ();	
		} 
		else if (this.name == "Cyclops_FullPrefab(Clone)") {
			cyclopControl = GetComponent<Cyclops_MasterControls> (); 
		}
		else{
			skeleControl = GetComponent<Master_Control> ();
		}
        currentHitPoints = hitPoints;
        maxHealth = hitPoints;
	}
		
	public void setHealth(int health){
		currentHitPoints = health;
	}
	
    [PunRPC]
    void TakeDamage(int amt)
    {
        currentHitPoints -= amt;
        if(currentHitPoints > maxHealth)
        {
            currentHitPoints = maxHealth;
        }
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
