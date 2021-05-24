using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsItemMotor : MonoBehaviour
{
    public OptionsDirector director;
    public string Action;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void isPressed()
    {
        director.StartOption(Action);
    }

}
