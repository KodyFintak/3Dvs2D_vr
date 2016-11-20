using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUI : MonoBehaviour {

	[Tooltip("Pixel offset from the player target")]
	public Vector3 ScreenOffset = new Vector3 (0f, 30f, 0f);

	public Text PlayerNameText;
	public Slider PlayerHealthSlider;


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
			PlayerNameText.text = _target.photonView.owner.name;
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
			PlayerHealthSlider.value = _target.getHealth();
		}
		if (_target == null) {
			Destroy (this.gameObject);
			return;
		}
	}
	void Awake()
	{
		this.GetComponent<Transform> ().SetParent (GameObject.Find ("Canvas").GetComponent<Transform> ());
	}
}
