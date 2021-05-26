using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OpenningDirector : MonoBehaviour
{

    public TextMeshProUGUI Moonshire_Text;
    public TextMeshProUGUI Presents_Text;

    public TextMeshProUGUI Garras_Text;
    public TextMeshProUGUI E_Text;
    public TextMeshProUGUI Penas_Text;

    public CanvasGroup Moonshire_Logo;
    public GameObject Name_Game;

    private float TimerToFadeOut_Logo = 4f;
    private float TimerToFadeIn_Game = 6f;
    private float TimerToFadeOut_Game = 8f;

    void Start()
    {
        StartCoroutine(DoFadeLogo(true, -1, 0.1f));


    }

    void Update()
    {

        TimerToFadeOut_Logo -= Time.deltaTime;
        TimerToFadeOut_Game -= Time.deltaTime;

        if (TimerToFadeOut_Logo <= 0)
        {
            StartCoroutine(DoFadeLogo(false, 0.1f, - 1));
            
        }

        if (TimerToFadeIn_Game <= 0)
        {
            Name_Game.SetActive(true);
        }

        if (TimerToFadeOut_Game <= 0)
        {
            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("2_Menu");
        }

    }

    private float Duration = 2f;

    IEnumerator DoFadeLogo(bool fadeIn, float start, float end)
    {
       
        float counter = 0f;

        while (counter < Duration)
        {
            counter += Time.deltaTime;
            float lerp = Mathf.Lerp(start, end, counter / Duration);

            Moonshire_Text.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, lerp);
            Presents_Text.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, lerp);
            Moonshire_Logo.alpha = Mathf.Lerp(fadeIn ? 0 : 1, fadeIn ? 1 : 0, counter / Duration);
            yield return null;
        }

    }

    IEnumerator DoFadeGame(float start, float end)
    {

        float counter = 0f;

        while (counter < Duration)
        {
            counter += Time.deltaTime;

            float lerp = Mathf.Lerp(start, end, counter / Duration);

            Color color = Garras_Text.color;
            color.a = lerp;

            Garras_Text.color = color;
            E_Text.color = color;
            Penas_Text.color = color;

            yield return null;
        }

    }

}
