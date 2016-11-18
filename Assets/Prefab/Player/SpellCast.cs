using UnityEngine;
using System.Collections;

public class SpellCast : MonoBehaviour {

	public Transform spawnLocation;
	//public GameObject spell;
	public int spellspeed;



	void Update () {

	}

	public void spellCast(){
		GameObject magic_spell = PhotonNetwork.Instantiate("My_Spell", spawnLocation.transform.position, spawnLocation.rotation,0) as GameObject;
		magic_spell.GetComponent<Rigidbody>().AddForce(spawnLocation.forward * spellspeed);
	}
}