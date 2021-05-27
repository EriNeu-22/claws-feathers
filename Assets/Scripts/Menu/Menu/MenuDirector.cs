using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuDirector : MonoBehaviour
{
    private GameObject GameManager;

    public GameObject[] itens;
    public GameObject MenuBox;
    public CanvasGroup fade;
     
    private string NextScene;
    private bool GoToNextScene = false;

    private float timerToFade = 1f;
    private CanvasScaler canvas;
    private CanvasGroup canvasGroup;

    private float TimerToFadeIn = 0f;

    private AudioSource AudioMenuTheme;

    void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager");

        canvas = GetComponent<CanvasScaler>();
        canvasGroup = MenuBox.GetComponent<CanvasGroup>();

        StartCoroutine(DoFade(canvasGroup, canvasGroup.alpha, 1, 0.5f));


        if (PlayerPrefs.GetString("PreviousScene").Equals("1_Introduction"))
        {
            TimerToFadeIn = 2f;
            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            fade.alpha = 1;
            StartCoroutine(DoFade(fade, fade.alpha, 0, TimerToFadeIn));
        } else
        {
            fade.alpha = 0;
        }


    }

    void Update()
    {

        if(TimerToFadeIn > 0)
        {
           TimerToFadeIn -= Time.deltaTime;

        } else
        {
            fade.gameObject.SetActive(false);
        }


            if (GoToNextScene)
            {
                timerToFade -= Time.deltaTime;
                if (timerToFade <= 0)
                {
                    StartGame();
                }

                FreezeMenu();
            }

    }

    public void GoToScene(string scene)
    {
        NextScene = scene;
        GoToNextScene = true;

    }

    #region START_GAME
    private const string START_GAME = "7_TrainingField";
    private const string OPTION_GAME = "3_Options";
    private const string QUIT_GAME = "Quit_Game";
    public GameObject SceneLoaderMenu;
    private bool alreadyFade = false;
    private float timerToTransition = 1.4f;

    private void StartGame()
    {

        if (NextScene.Equals(START_GAME) && !alreadyFade)
        {
            var sceneLoaderMenu = Instantiate(SceneLoaderMenu, new Vector3(canvas.referenceResolution.x / 2, canvas.referenceResolution.y / 2, 0), Quaternion.identity);
            sceneLoaderMenu.transform.SetParent(transform);
            alreadyFade = true;
            GameManager.SendMessage("StopMenuSong", true);
        }

        if (NextScene.Equals(OPTION_GAME))
        {

            StartCoroutine(DoFade(canvasGroup, canvasGroup.alpha, 0, 0.5f));
            alreadyFade = true;
        }

        if (NextScene.Equals(QUIT_GAME))
        {
            Application.Quit();
        }

        ChangeoOfScene();


    }

    #endregion

    private void FreezeMenu()
    {
        foreach (GameObject item in itens)
        {
            item.GetComponent<Button>().interactable = false;
            item.SendMessage("Stop", true);
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


    private void ChangeoOfScene()
    {

        if (alreadyFade)
        {
            timerToTransition -= Time.deltaTime;
            if (timerToTransition <= 0)
            {
                PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
                SceneManager.LoadScene(NextScene);
                
            }
        }
    }

    public void GoToMoonshireWebSite()
    {
        Application.OpenURL("https://moonshire.herokuapp.com/");
    }

}
