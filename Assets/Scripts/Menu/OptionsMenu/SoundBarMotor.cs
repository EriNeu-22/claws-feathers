using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SoundBarMotor : MonoBehaviour
{
    public TMP_Text VolumeText;
    public OptionsDirector director;
    public RectTransform rectTransform;

    public Sprite[] images;
    public Image VolumeOptionImage;


    private int VolumeSound;

    private bool IsSelected = false;

    private Vector3 position;
    private float posY = 0;
    private bool HoldVolumeOption = false;

    private float minVolumePos = 973f;
    private float maxVolumePos = 1167f;

    private const int MAX_VOLUME = 100;
    private const int OP_NOT_SELECTED = 0;
    private const int OP_SELECTED = 1;
    private const int OP_MUTE_NOT_SELECTED = 2;
    private const int OP_MUTE_SELECTED = 3;

    private bool ChekIfMouseAlreadyOver = true;
    private bool MouseAlreadyOver = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        VolumeSound = director.VolumeSound;
        position = rectTransform.transform.position;
        position.x = minVolumePos + GetVolumePos(VolumeSound);
        posY = position.y;

        rectTransform.transform.position = position;
        VolumeText.text = "" + VolumeSound;

    }

    void Update()
    {
        CheckIfButtonIsSelected();

        ChekIfMouseAlreadyOver = !HoldVolumeOption;

        if (ChekIfMouseAlreadyOver)
        {
            MouseAlreadyOver = CheckIfMouseIsOverBallSoundBar();
            ChekIfMouseAlreadyOver = !MouseAlreadyOver;
        }

        HoldVolumeOption = IsSelected && MouseAlreadyOver;

        if (HoldVolumeOption)
        {
            ChangeVolume();
        }

        SelectOption();

    }

    private void SelectOption()
    {

        VolumeOptionImage.sprite = images[OP_NOT_SELECTED];

        if (HoldVolumeOption)
            VolumeOptionImage.sprite = images[OP_SELECTED];

        if (VolumeSound == 0)
        {
            if (HoldVolumeOption)
            {
                VolumeOptionImage.sprite = images[OP_MUTE_SELECTED];
            }
            else
            {
                VolumeOptionImage.sprite = images[OP_MUTE_NOT_SELECTED];
            }
        }

    }

    private void ChangeVolume()
    {
        position = Input.mousePosition;
        if (position.x > maxVolumePos)
        {
            position.x = maxVolumePos;
        }

        if (position.x < minVolumePos)
        {
            position.x = minVolumePos;
        }

        position.y = posY;

        rectTransform.transform.position = position;
        VolumeSound = GetVolumeValue(position.x);
        director.VolumeSound = VolumeSound;

        if (VolumeSound < 10)
        {
            VolumeText.text = "" + VolumeSound;
        }
        else
        {
            VolumeText.text = "" + VolumeSound;
        }
        
    }

    public void CheckIfButtonIsSelected()
    {
        if (Input.GetMouseButtonDown(0))
            IsSelected = true;

        if (Input.GetMouseButtonUp(0))
            IsSelected = false;
    }

    private bool CheckIfMouseIsOverBallSoundBar()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastsThatMouseIsOver = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastsThatMouseIsOver);
        
        foreach(RaycastResult raycast in raycastsThatMouseIsOver){
            if(raycast.gameObject == gameObject)
            {
                return true;
            }
        }

        return false;
    }

    private int GetVolumeValue(float position)
    {
        float volumeIni = MAX_VOLUME * (position - minVolumePos) / (maxVolumePos - minVolumePos);
        int volume = (int) Mathf.Round(volumeIni);
        return volume;
    }

    private float GetVolumePos(int volume)
    {
        float volumePos =  (maxVolumePos - minVolumePos)  * volume / MAX_VOLUME;
        return volumePos;
    }

}
