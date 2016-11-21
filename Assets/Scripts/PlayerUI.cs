using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerUI : MonoBehaviour {

	[Tooltip("Pixel offset from the player target")]
	public Vector3 ScreenOffset = new Vector3 (0f, 3f, 0f);

	public Text PlayerNameText;
	public Slider PlayerHealthSlider;
    public Slider PlayerManaSlider;
    public Slider PlayerKeySlider;


    Player _target;
	float _characterControllerHeight = 0f;
	Transform _targetTransform;
	Vector3 _targetPosition;


	public void SetTarget(Player target){
		if (target == null) {
			Debug.LogError ("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
			return;
		}
		_target = target;
		CharacterController _characterController = _target.GetComponent<CharacterController> ();
		if (_characterController != null) {
			_characterControllerHeight = _characterController.height;
		}
		if (PlayerNameText != null) {
            //PlayerNameText.text = _target.photonView.owner.name;
            List<string> names = new List<string>();
            names.Add("Capt. America");
            names.Add("Sgt. Harambae");
            names.Add("Lt. West Nile");
            names.Add("Trump Wins");
            string[] names2 = names.ToArray();
            string name = names2[UnityEngine.Random.Range(0, names2.Length)];

            PlayerNameText.text = name;
		}
	}

	void LateUpdate(){
		if (_targetTransform!=null)
		{
			_targetPosition = _targetTransform.position;
			_targetPosition.y += _characterControllerHeight;
			this.transform.position = Camera.main.WorldToScreenPoint (_targetPosition) + ScreenOffset;
		}
	}

	void Update()
	{
		if (PlayerHealthSlider != null) {
			//PlayerHealthSlider.value = _target.getHealth();
        }
        if (PlayerManaSlider != null)
        {
           // PlayerManaSlider.value = _target.getMana();
        }
        if (PlayerManaSlider != null)
        {
           // PlayerXPSlider.value = _target.getXP();
        }
        if (_target == null) {
			Destroy (this.gameObject);
			return;
		}
	}
	void Awake()
	{
        if(!PhotonNetwork.isMasterClient)
		    this.GetComponent<Transform> ().SetParent (GameObject.Find ("Canvas").GetComponent<Transform> ());
	}
}
