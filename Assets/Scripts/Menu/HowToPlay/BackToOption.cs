using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToOption : MonoBehaviour
{
    public HowToPlayDirector director;
    public string Action;

    public void isPressed()
    {
        director.StartOption(Action);

    }

}
