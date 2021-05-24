using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dialogue : MonoBehaviour
{

    public Sprite[] profiles;
    public string[] speechText;
    public string[] actorsName;

    public LayerMask Player;

    private bool startInteraction;
    public bool NotInteract = true;

    public DialogueControl dialogueControl;

    private void Update()
    {

        if (startInteraction && NotInteract)
        {
            dialogueControl.Speech(profiles, speechText, actorsName);
            dialogueControl.Fade(startInteraction);
            NotInteract = false;

        }

    }

    public void Interact(bool startInteract)
    {

        startInteraction = startInteract;

    }

}
