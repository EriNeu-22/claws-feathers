using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuDirector : MonoBehaviour
{

    public GameObject[] itens;
    public GameObject MenuBox;
     
    private string NextScene;
    private bool GoToNextScene = false;

    private float timerToFade = 1f;
    private CanvasScaler canvas;
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvas = GetComponent<CanvasScaler>();
        canvasGroup = MenuBox.GetComponent<CanvasGroup>();
        StartCoroutine(DoFade(canvasGroup, canvasGroup.alpha, 1));
    }

    void Update()
    {

        if (GoToNextScene)
        {
            timerToFade -= Time.deltaTime;
            if (timerToFade <= 0) {
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
        }

        if (NextScene.Equals(OPTION_GAME))
        {
            StartCoroutine(DoFade(canvasGroup, canvasGroup.alpha, 0));
            alreadyFade = true;
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

    private float Duration = 0.5f;
    
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


    private void ChangeoOfScene()
    {

        if (alreadyFade)
        {
            timerToTransition -= Time.deltaTime;
            if (timerToTransition <= 0)
            {
                SceneManager.LoadScene(NextScene);
            }
        }
    }

}
