using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DirectorFase2_1 : MonoBehaviour
{
    public GameManager GameManager;

    private int goal;
    public GameObject player;
    public PlayerAtHome playerMotor;
    public GameObject mentor;
    public CanvasGroup canvasGroup;
    public CanvasScaler canvas;

    public GameObject MenuPause;

    public GameObject WoodWall;
    private int scoreBefore = 0;
    private int score = 0;

    private const string ENEMY_TAG = "enemy";

    private bool completeFase = false;

    private bool PauseAction = false;

    private bool GameOver = false;
    public GameObject DamageHud;
    public SpriteRenderer DamageBar;


    public AudioSource[] audios;
    private const float AudioEnviromentVolumMax = 0.25f;
    private const int AudioEnviroment = 0;
    private const float AudioFightVolumeMax = 0.75f;
    private const int AudioFight = 1;


    private void UpdateAudios()
    {
        audios[AudioFight].volume = AudioFightVolumeMax * GameManager.AudioVolumePerc / 100;
        audios[AudioEnviroment].volume = AudioEnviromentVolumMax * GameManager.AudioVolumePerc / 100;

    }

    void Start()
    {
        GameObject[] enemiesVector = GameObject.FindGameObjectsWithTag(ENEMY_TAG);
        goal = enemiesVector.Length;
        StartCoroutine(DoFade(canvasGroup, canvasGroup.alpha, 0, 3f));

       audios[AudioEnviroment].Play();
        
    }

    private bool AlreadyPaused = false;
    public GameObject SceneLoaderMenu;


    private float timerToReloadScene = 2f;

    void Update()
    {

        if (playerMotor.NumberOfInteractions == 2)
        {
            RunAudio(AudioFight, true);
            WoodWall.SetActive(true);
        }

        UpdateAudios();

        if (GameOver)
        {
            StartCoroutine(DoFade(canvasGroup, canvasGroup.alpha, 1, 1f));
            StartCoroutine(DoFadeBar(DamageBar, DamageBar.color.a, 0, 1f));

            DamageHud.SetActive(true);

            timerToReloadScene -= Time.deltaTime;
            if (timerToReloadScene <= 0)
            {
                SceneManager.LoadScene("9_AtHome");
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape) && !playerMotor.prince.IsInteracting)
        {
            PauseAction = !PauseAction;
            player.SendMessage("PauseGame", PauseAction);
            MenuPause.SetActive(PauseAction);
        }

        if (PauseAction)
        {
        }

        if (!playerMotor.prince.IsAlive)
        {
            GameOver = true;
        }

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
    private void RunAudio(int id, bool turnOn)
    {
        if (turnOn)
        {
            if (!audios[id].isPlaying)
                audios[id].Play();

        }
        else
        {
            audios[id].Pause();
        }
    }

}
