using UnityEngine;
using System.Collections;

public class SpellCast : MonoBehaviour {

	public Transform spawnLocation;
	public GameObject spell;
	public int spellspeed;



	void Update () {

	}

	public void spellCast(){
		GameObject magic_spell = Instantiate(spell, spawnLocation.transform.position, spawnLocation.rotation) as GameObject;
		magic_spell.GetComponent<Rigidbody>().AddForce(spawnLocation.forward * spellspeed);
	}
}