using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdiomSelector : MonoBehaviour
{

    public string NickLanguage;
    public Sprite[] IMG;
    public OptionsDirector director;
    
    private const int NOT_SELECTED = 0;
    private const int SELECTED = 1;

    public GameObject anotherButton;
    private Image image;
    private Button button;


    private void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();

    }

    private void Update()
    {
        if (director.Language.Equals(NickLanguage))
        {
            Enable(true);
        }
        else
        {
            Disable(true);
        }
    }

    public void ChangeLanguage()
    {
        director.Language = NickLanguage;
    }

    private void Disable(bool disable)
    {
        image.sprite = IMG[NOT_SELECTED];
        button.enabled = disable;
    }

    private void Enable(bool enable)
    {
        image.sprite = IMG[SELECTED];
        button.enabled = !enable;
    }


}
