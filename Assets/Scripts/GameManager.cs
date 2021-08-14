using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private float TimerToStartMenuSong = 6f;
    private float TimerCounterToFadeMenuSong = 0f;
    private bool AlreadyStartSong = false;

    private AudioSource _AudioMenuTheme;

    private const float AudioSelectedVolume = 0.01f;
    private const float AudioPressedVolume = 0.06f;
    private const float AudioMenuThemeVolumeMax = 0.8f;
    private float AudioMenuThemeVolume = 0.8f;

    public static float AudioVolumePerc = 100;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {

        _AudioMenuTheme = GetComponent<AudioSource>();
        _AudioMenuTheme.volume = 0;
    }

    void Update()
    {
        ManageMenuThemeSong();

    }

    public void ManageMenuThemeSong()
    {
        TimerToStartMenuSong -= Time.deltaTime;
        if (TimerToStartMenuSong <= 0 && !AlreadyStartSong)
        {
            _AudioMenuTheme.Play();
            AlreadyStartSong = true;
        }

        if (AlreadyStartSong)
        {
            if (TimerCounterToFadeMenuSong < 10f)
            {
                _AudioMenuTheme.volume = Mathf.Lerp(0, AudioMenuThemeVolume, TimerCounterToFadeMenuSong / 10f);
                TimerCounterToFadeMenuSong += Time.deltaTime;
            }
            else
            {
                AudioMenuThemeVolume = AudioMenuThemeVolumeMax * AudioVolumePerc / 100;
                _AudioMenuTheme.volume = AudioMenuThemeVolume;
            }
        }



    }

    public void StopMenuSong(bool stop)
    {
        _AudioMenuTheme.Pause();

    }


}
