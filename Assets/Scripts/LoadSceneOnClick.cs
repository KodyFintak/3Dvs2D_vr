using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;

public class LoadSceneOnClick : MonoBehaviour {

	public GameObject player;

	public void LoadByIndex(int sceneIndex)
	{
		Instantiate( player);
	}

}
