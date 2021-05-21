using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentorMotor : MonoBehaviour
{

    [Header("Components")]
    public GameObject firstStrawman;
    public GameObject score;
    public GameObject [] interactions;
    private int index = 0;


    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if(FirstStrawmanIsDestroyed() && index == 0)
        {
            interactions[index].SendMessage("Interact", true);
            score.SetActive(true);
            index++;
        }


    }

    private bool FirstStrawmanIsDestroyed()
    {
        return firstStrawman == null;
    }

}
