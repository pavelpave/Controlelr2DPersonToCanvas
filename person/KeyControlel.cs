using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyControlel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject PlayerObject;
    public GameObject L_Btn;
    public GameObject R_Btn;
    public GameObject Jump_Btn;

    private PlayerCs _PlayerControler;
    private float _animPositonUi = 3f;

    public void OnPointerEnter(PointerEventData pointEventData)
    {
        if (_PlayerControler != null)
        {
            if (pointEventData.pointerEnter == R_Btn)
            {
                ButtonPositionAmimation(R_Btn, true);
                _PlayerControler.MoveRight();
            }
            else if (pointEventData.pointerEnter == L_Btn)
            {
                ButtonPositionAmimation(L_Btn, true);
                _PlayerControler.MoveLeft();
            }
            else if (pointEventData.pointerEnter == Jump_Btn)
            {
                ButtonPositionAmimation(Jump_Btn, true);
                _PlayerControler.Jump();
            }
        }

    }

    public void OnPointerExit(PointerEventData pointEventData)
    {
        if (_PlayerControler != null)
        {
            _PlayerControler.Idle();
            if (pointEventData.pointerEnter == R_Btn)
            {
                ButtonPositionAmimation(R_Btn, false);
            }
            else if (pointEventData.pointerEnter == L_Btn)
            {
                ButtonPositionAmimation(L_Btn, false);
            }
            else if (pointEventData.pointerEnter == Jump_Btn)
            {
                ButtonPositionAmimation(Jump_Btn, false);
            }
        }
    }

    private void ButtonPositionAmimation(GameObject _uiElement, bool _isPositiv)
    {
        if (_isPositiv)
        {
            _uiElement.transform.position = new Vector3(_uiElement.transform.position.x, _uiElement.transform.position.y + _animPositonUi, _uiElement.transform.position.z);
        }
        else
        {
            _uiElement.transform.position = new Vector3(_uiElement.transform.position.x, _uiElement.transform.position.y - _animPositonUi, _uiElement.transform.position.z);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _PlayerControler = PlayerObject.GetComponent<PlayerCs>();
        if (_PlayerControler == null)
        {
            Debug.LogError("_PlayerControler is null!");
        }
        Debug.Log(_PlayerControler);
    }

    // Update is called once per frame
    void Update()
    {

    }
}