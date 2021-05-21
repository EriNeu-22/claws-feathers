using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueControl : MonoBehaviour
{
    [Header("Components")]
    public GameObject player;
    public GameObject dialogueObj;
    public Image profile;
    public TMP_Text speechText;
    public TMP_Text actorNameText;

    [Header("Settings")]
    public float typingSpeed;
    private string[] sentences;
    private string[] actors;
    private Sprite[] splashes;
    private int index;

    private bool IsInteracting = false;

    void Update()
    {
        if (IsInteracting && Input.GetKeyDown(KeyCode.Return))
        {
            NextSentence();
            
        }

    }

    public void Speech(Sprite[] splashesArts, string[] txt, string[] actorsName)
    {
        IsInteracting = true;
        player.SendMessage("PlayerIsTalking", IsInteracting);
        
        dialogueObj.SetActive(IsInteracting);

        sentences = txt;
        splashes = splashesArts;
        actors = actorsName;
        StartCoroutine(TypeSentences());

    }

    IEnumerator TypeSentences()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            if (_Next)
            {
                _Next = false;
                break;
            }
            speechText.text += letter;
            profile.sprite = splashes[index];
            actorNameText.text = actors[index];
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private bool _Next = false;

    public void NextSentence()
    {
        if (speechText.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentences());
            }
            else
            {
                speechText.text = "";
                index = 0;
                IsInteracting = false;
                player.SendMessage("PlayerIsTalking", IsInteracting);
                dialogueObj.SetActive(IsInteracting);
            }
        }
        else
        {
            speechText.text = sentences[index];
            _Next = true;
        }

    }



}
