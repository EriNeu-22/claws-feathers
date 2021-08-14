using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DirectorFase1 : MonoBehaviour
{
    public GameManager GameManager;

    private int goal;
    public TMP_Text scoreTmpText;
    public GameObject player;
    public PlayerMotor playerMotor;
    public GameObject mentor;
    public CanvasGroup canvasGroup;
    public CanvasScaler canvas;

    public GameObject MenuPause;

    private GameObject strawman;
    private int scoreBefore = 0;
    private int score = 0;

    private const string ENEMY_TAG = "enemy";

    private bool completeFase = false;

    private bool PauseAction = false;

    public GameObject River;

    private bool GameOver = false;
    public GameObject DamageHud;
    public SpriteRenderer DamageBar;


    void Start()
    {

        foreach (AudioSource audio in audios)
        {
            audio.Play();
        }

        GameObject[] enemiesVector = GameObject.FindGameObjectsWithTag(ENEMY_TAG);
        goal = enemiesVector.Length;
        StartCoroutine(DoFade(canvasGroup, canvasGroup.alpha, 0, 3f));

    }

    private bool AlreadyPaused = false;
    public GameObject SceneLoaderMenu;


    private float timerToReloadScene = 2f;

    void Update()
    {
        if (GameOver)
        {
            Debug.Log("cheguei");
            StartCoroutine(DoFade(canvasGroup, canvasGroup.alpha, 1, 1f));
            StartCoroutine(DoFadeBar(DamageBar, DamageBar.color.a, 0, 1f));

            DamageHud.SetActive(true);

            timerToReloadScene -= Time.deltaTime;
            if (timerToReloadScene <= 0)
            {
                SceneManager.LoadScene("7_TrainingField");
            }

        }

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

        if (PauseAction)
        {
            updateAudios();
        }

        if (!playerMotor.prince.IsAlive)
        {
            GameOverSound();
            GameOver = true;
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

    private bool teste = false;
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

    IEnumerator DoFadeBar(SpriteRenderer sprRender, float start, float end, float Duration)
    {
        float counter = 0f;

        while (counter < Duration)
        {
            counter += Time.deltaTime;
            float a = Mathf.Lerp(start, end, counter / Duration);
            Color color = new Color(sprRender.color.r, sprRender.color.g, sprRender.color.b, a);
            sprRender.color = color;
            yield return null;
        }

    }

    public AudioSource[] audios;
    private const float AudioTrainingFieldMax = 0.12f;
    private const float AudioForestEnviromentVolumMax = 0.86f;
    private const float AudioRiverVolumMax = 0.86f;
    private const int AudioTrainingField = 0;
    private const int AudioForestEnviromentVolum = 1;
    private const int AudioRiverVolum = 2;

    private void updateAudios()
    {
        audios[AudioTrainingField].volume = AudioTrainingFieldMax * GameManager.AudioVolumePerc / 100;
        audios[AudioForestEnviromentVolum].volume = AudioForestEnviromentVolumMax * GameManager.AudioVolumePerc / 100;
        audios[AudioRiverVolum].volume = AudioRiverVolumMax * GameManager.AudioVolumePerc / 100;
    }

    public void GameOverSound()
    {
        audios[AudioTrainingField].Pause();
        audios[AudioForestEnviromentVolum].Pause();
        audios[AudioRiverVolum].Pause();

        audios[AudioForestEnviromentVolum].volume = AudioForestEnviromentVolumMax * GameManager.AudioVolumePerc / 100;
    }

    private void UpdateSoundOfRiver(){

        float DistanceX = player.transform.position.x - River.transform.position.x;
        DistanceX = DistanceX < 0 ? DistanceX * (-1) : DistanceX;
        float DistanceY = player.transform.position.y - River.transform.position.y;
        DistanceY = DistanceY < 0 ? DistanceY * (-1) : DistanceY;

        if(DistanceX < DistanceY)
        {

            audios[AudioRiverVolum].volume = AudioRiverVolumMax * GameManager.AudioVolumePerc / 100;

        } else
        {

        }


    }

}
