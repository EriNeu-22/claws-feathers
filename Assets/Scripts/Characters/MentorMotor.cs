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

    private bool alreadyDestroyed = false;
    private bool FirstStrawmanDestroyed = false;

    private bool playerRocks;


    void FixedUpdate()
    {

        if(FirstStrawmanDestroyed && !alreadyDestroyed && !playerRocks)
        {
            interactions[index].SendMessage("Interact", true);
            score.SetActive(true);
            alreadyDestroyed = true;
            index++;
        }

        if (playerRocks)
        {
            index = 1;
            interactions[index].SendMessage("Interact", true);
            score.SetActive(true);
            alreadyDestroyed = true;
            
        }


    }

    public void FirstStrawmanIsDestroyed(bool isDestroyed)
    {
        FirstStrawmanDestroyed = isDestroyed;
    }



    public void PlayerRocks(bool playerWin)
    {
        playerRocks = playerWin;
    }


}
