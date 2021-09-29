using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayDirector : MonoBehaviour
{
    public GameManager GameManager;

    public string Language;
    public int VolumeSound;

    private string Action;
    private bool actionTrigger;

    private const string OPTIONS_SCENE = "3_Options";

    public CanvasGroup KeyboardCanvasGroup;

    void Start()
    {
        StartCoroutine(DoFade(KeyboardCanvasGroup, KeyboardCanvasGroup.alpha, 1, 1f));
        actionTrigger = false;
    }

    void Update()
    {
        GameManager.AudioVolumePerc = VolumeSound;

         DoMenuOptions();
       
    }

    public void StartOption(string _action)
    {
        Action = _action;
        actionTrigger = true;
    }

    private float timerToFade = 1f;
    private float Duration = 0.6f;

    private void DoMenuOptions()
    {
        if (actionTrigger)
        {
            Debug.Log("dsfd");
            if (Action.Equals(OPTIONS_SCENE))
            {
                StartCoroutine(DoFade(KeyboardCanvasGroup, KeyboardCanvasGroup.alpha, 0, 1f));

                timerToFade -= Time.deltaTime;
                if (timerToFade <= 0)
                {
                    timerToFade = Duration;
                    SceneManager.LoadScene(OPTIONS_SCENE);
                }
            }

        }
    }

    IEnumerator DoFade(CanvasGroup canvasGroup, float start, float end, float Duration)
    {
        float counter = 0f;

        while (counter < Duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / Duration);
            yield return null;
        }

    }

}
