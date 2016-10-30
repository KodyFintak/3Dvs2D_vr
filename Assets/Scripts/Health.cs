using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public int hitPoints = 100;
    int currentHitPoints;

	// Use this for initialization
	void Start () {
        currentHitPoints = hitPoints;
	}
	
    [PunRPC]
    void TakeDamage(int amt)
    {
        currentHitPoints -= amt;

        if(currentHitPoints <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
