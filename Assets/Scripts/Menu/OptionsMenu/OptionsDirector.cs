using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsDirector : MonoBehaviour
{

    public GameManager GameManager;

    public string Language;
    public int VolumeSound;

    public GameObject OptionBox;

    private string Action;
    private bool actionTrigger;

    private const string MENU_SCENE = "2_Menu";
    private const string BACK_TO_MENU = "Back_to_Menu";

    private CanvasGroup canvasgroup;

    public string MenuType;

    private bool PauseActive = false;

    void Start()
    {
        if (MenuType.Equals("MenuOptions"))
        {
            canvasgroup = OptionBox.GetComponent<CanvasGroup>();
            StartCoroutine(DoFade(canvasgroup, canvasgroup.alpha, 1));
            actionTrigger = false;
        }

    }

    void Update()
    {
        GameManager.AudioVolumePerc = VolumeSound;

        if (MenuType.Equals("MenuOptions"))
        {
            DoMenuOptions();
        }

    }

    private float timerToFade = 1f;
    private float Duration = 0.6f;

    IEnumerator DoFade(CanvasGroup canvasGroup, float start, float end)
    {
        float counter = 0f;

        while (counter < Duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / Duration);
            yield return null;
        }

    }


    public void StartOption(string _action)
    {
        Action = _action;
        actionTrigger = true;
    }

    private void DoMenuOptions()
    {
        if (actionTrigger)
        {

            if (Action.Equals(BACK_TO_MENU))
            {
                StartCoroutine(DoFade(canvasgroup, canvasgroup.alpha, 0));

                timerToFade -= Time.deltaTime;
                if (timerToFade <= 0)
                {
                    timerToFade = Duration;
                    SceneManager.LoadScene(MENU_SCENE);
                }
            }

        }
    }

}
