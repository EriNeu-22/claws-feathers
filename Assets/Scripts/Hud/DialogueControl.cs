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

    private bool _Next = false;
    private bool IsInteracting = false;

    void Start()
    {
        dialogueBoxPos = transform.position;
    }
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
        
        dialogueObj.SetActive(true);
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
                SkipDialogue();
            }
        }
        else
        {
            speechText.text = sentences[index];
            _Next = true;
        }

    }

    public void SkipDialogue()
    {
        speechText.text = "";
        index = 0;
        IsInteracting = false;
        player.SendMessage("PlayerIsTalking", IsInteracting);
        dialogueObj.SetActive(IsInteracting);
        transform.position = dialogueBoxPos;
        Fade(IsInteracting);

    }


    private bool mFaded;
    private float Duration = 0.4f;
    private Vector3 dialogueBoxPos;

    public void Fade(bool mFaded)
    {
        var canvGroup = GetComponent<CanvasGroup>();

        if (mFaded)
        {
            StartCoroutine(DoFade(canvGroup, canvGroup.alpha, 1));
        }
        else
        {
            canvGroup.alpha = 0;
        }

    }

    public IEnumerator DoFade(CanvasGroup canvasGroup, float start, float end)
    {
        float counter = 0f;
        int velocitySpawn = 4;

        while (counter < Duration)
        {
            if (end == 1)
            {
                Vector3 position = transform.position;
                position.y += velocitySpawn;
                transform.position = position;
            }

            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / Duration);
            yield return null;
        }
    }


}
