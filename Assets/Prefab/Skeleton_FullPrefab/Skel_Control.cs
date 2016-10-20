using UnityEngine;
using System.Collections;

// this class has basic properties for the Full_Skeleton prefab that will be called throughout AI scripts

public class Skel_Control : MonoBehaviour {

	public void setRun(Animator animate){
		animate.SetBool ("isRunning", true);
		animate.SetBool ("isWalk", false);
		animate.SetBool ("isIdle", false);
		animate.SetBool ("isAttack", false);
		animate.SetBool ("isDam", false);
	}

	public void setAttack(Animator animate){
		animate.SetBool ("isAttack", true);
		animate.SetBool ("isRunning", false);
		animate.SetBool ("isWalk", false);
		animate.SetBool ("isIdle", false);
		animate.SetBool ("isDam", false);
	}

	public void setIdle(Animator animate){
		animate.SetBool ("isIdle", true);
		animate.SetBool ("isAttack", false);
		animate.SetBool ("isRunning", false);
		animate.SetBool ("isWalk", false);
		animate.SetBool ("isDam", false);
	}

	public void setWalk(Animator animate){
		animate.SetBool ("isWalk", true);
		animate.SetBool ("isIdle", false);
		animate.SetBool ("isAttack", false);
		animate.SetBool ("isRunning", false);
		animate.SetBool ("isDam", false);
	}

//	public void setDamage(Animator animate){
//		animate.SetBool ("isDam", true);
//		animate.SetBool ("isWalk", false);
//		animate.SetBool ("isIdle", false);
//		animate.SetBool ("isAttack", false);
//		animate.SetBool ("isRunning", false);
//	}

	public void setDeath(Animator animate){
		animate.SetBool ("isDeath", true);
		animate.SetBool ("isDam", false);
		animate.SetBool ("isWalk", false);
		animate.SetBool ("isIdle", false);
		animate.SetBool ("isAttack", false);
		animate.SetBool ("isRunning", false);
	}
}
