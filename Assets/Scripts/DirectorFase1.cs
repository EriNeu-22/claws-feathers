using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DirectorFase1 : MonoBehaviour
{
    private int goal;
    public TMP_Text scoreTmpText;
    public GameObject player;
    public PlayerMotor playerMotor;
    public GameObject mentor;
    public CanvasGroup canvasGroup;

    public GameObject MenuPause;

    private GameObject strawman;
    private int scoreBefore = 0;
    private int score = 0;

    private const string ENEMY_TAG = "enemy";

    private bool completeFase = false;

    public AudioSource[] audios;
    private const int AudioTrainingField = 0;
    private const int AudioForestEnviroment = 1;

    private bool PauseAction = false;

    void Start()
    {
        audios[AudioTrainingField].Play();
        audios[AudioForestEnviroment].Play();

        GameObject[] enemiesVector = GameObject.FindGameObjectsWithTag(ENEMY_TAG);
        goal = enemiesVector.Length;
        StartCoroutine(DoFade(canvasGroup, canvasGroup.alpha, 0));

    }

    private bool AlreadyPaused = false;

    void Update()
    {

        string text = GetTextScore();
        scoreTmpText.SetText(text);

        if(scoreBefore < score)
            scoreBefore = score;

        if(score == goal)
        {
            mentor.SendMessage("PlayerRocks", true);
        }

        if(score == 1)
        {
            mentor.SendMessage("FirstStrawmanIsDestroyed", true);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !playerMotor.prince.IsInteracting)
        {
            PauseAction = !PauseAction;
            player.SendMessage("PauseGame", PauseAction);
            MenuPause.SetActive(PauseAction);
        }

    }

    public void Score(GameObject strawmanObj) {

        strawman = strawmanObj;
        score += 1;
        strawman.GetComponent<Collider2D>().enabled = false;
        Destroy(strawman, 3f);
     
    }

    private string GetTextScore()
    {
        return "" + score + " / " + goal;
    }

    private float Duration = 3f;

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


}
