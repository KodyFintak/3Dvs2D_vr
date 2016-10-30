using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DungeoneerUI : MonoBehaviour
{

    #region Public Properties

    Player _target;
    [Tooltip("Pixel offset from the player target")]
    public Vector3 ScreenOffset = new Vector3(0f, 30f, 0f);
    [Tooltip("UI Text to display Player's Name")]
    public Text PlayerNameText;
    //[Tooltip("UI Slider to display Player's Health")]
    //public Slider PlayerHealthSlider;

    #endregion

    #region Private Properties
    float _characterControllerHeight = 0f;
    Transform _targetTransform;
    Vector3 _targetPosition;
    #endregion


    // Use this for initialization
    void Start()
    {

    }

    void Awake()
    {
        if(!PhotonNetwork.player.isMasterClient)
            this.GetComponent<Transform>().SetParent(GameObject.Find("Canvas").GetComponent<Transform>());
    }

    void LateUpdate()
    {
        // #Critical
        // Follow the Target GameObject on screen.
        if (_targetTransform != null)
        {
            _targetPosition = _targetTransform.position;
            _targetPosition.y += _characterControllerHeight;
            this.transform.position = Camera.main.WorldToScreenPoint(_targetPosition) + ScreenOffset;
        }
    }


    // Update is called once per frame
    void Update()
    {
        // Destroy itself if the target is null, It's a fail safe when Photon is destroying Instances of a Player over the network
        if (_target == null)
        {
            Destroy(this.gameObject);
            return;
        }

    }



    public void SetTarget(Player target)
    {
        if (target == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }
        // Cache references for efficiency
        _target = target;
        //if (PlayerNameText != null)
        //{
        //    PlayerNameText.text = _target.photonView.owner.name;
        //}
        CharacterController _characterController = _target.GetComponent<CharacterController>();
        // Get data from the Player that won't change during the lifetime of this Component
        if (_characterController != null)
        {
            _characterControllerHeight = _characterController.height;
        }

    }


}
