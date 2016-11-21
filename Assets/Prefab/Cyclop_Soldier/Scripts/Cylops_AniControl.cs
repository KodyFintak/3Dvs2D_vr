using UnityEngine;
using System.Collections;

public class Cylops_AniControl : MonoBehaviour {

	public void setRun(Animator animate){
		animate.SetBool ("isRun", true);
		animate.SetBool ("isAttack1", false);
		animate.SetBool ("isDead", false);
	}

	public void setAttack(Animator animate){
		animate.SetBool ("isAttack1", true);
		animate.SetBool ("isRun", false);
		animate.SetBool ("isDead", false);
	}

	public void setDeath(Animator animate){
		animate.SetBool ("isDead", true);
		animate.SetBool ("isAttack1", false);
		animate.SetBool ("isRun", false);
	}
}
