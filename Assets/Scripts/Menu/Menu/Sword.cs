using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class Sword : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameManager GameManager;
    public GameObject spearRight;
    public GameObject spearLeft;

    public float speedRotation;
    private bool IsPressed = false;
    private bool Highlighted = false;

    private Vector3 posLeftIni;
    private Vector3 posRightIni;

    public string Action;
    public MenuDirector director;

    private bool Continue = true;

    public AudioClip AudioClipSelected;
    private AudioSource AudioSelected;
    private float AudioSelectedVolume = 0.01f;

    public AudioClip AudioClipPressed;
    private AudioSource AudioPressed;
    private float AudioPressedVolume = 0.06f;

    void Start()
    {
        posLeftIni = spearLeft.transform.position;
        posRightIni = spearRight.transform.position;

        AudioSelected = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        AudioSelected.clip = AudioClipSelected;
        AudioSelected.volume = AudioSelectedVolume * GameManager.AudioVolumePerc / 100;

        AudioPressed = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        AudioPressed.clip = AudioClipPressed;
        AudioPressed.volume = AudioPressedVolume * GameManager.AudioVolumePerc / 100;

    }

    void Update()
    {
            Run();

    }

    public void Stop(bool stop)
    {
        Continue = false;
    }


    private void Run()
    {

        spearRight.SetActive(Highlighted);
        spearLeft.SetActive(Highlighted);

        if (IsPressed)
        {
            spearRight.transform.eulerAngles = new Vector3(0, spearRight.transform.eulerAngles.y, spearRight.transform.eulerAngles.z);
            spearLeft.transform.eulerAngles = new Vector3(0, spearLeft.transform.eulerAngles.y, spearLeft.transform.eulerAngles.z);

            Vector3 cutRight = spearRight.transform.position;
            Vector3 cutLeft = spearLeft.transform.position;

            if (posRightIni.x < cutLeft.x)
            {
                cutRight.x += cutRight.x * 0.5f * Time.deltaTime;
                spearRight.transform.position = cutRight;

                cutLeft.x -= cutLeft.x * 0.5f * Time.deltaTime;
                spearLeft.transform.position = cutLeft;

            }

        }
        else
        {
            if (Highlighted)
            {
                spearRight.SetActive(Continue);
                spearLeft.SetActive(Continue);

                Turn(spearRight);
                Turn(spearLeft);
            }
        }

    }


    public void Turn(GameObject gameObj)
    {

        gameObj.transform.Rotate(Vector3.right * speedRotation * Time.deltaTime);

    }

    public void Selected()
    {
        IsPressed = true;
        StartCoroutine(ExampleCoroutine());
        director.GoToScene(Action);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Highlighted = true;
        AudioSelected.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!IsPressed)
            Highlighted = false;
    }

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        AudioPressed.Play();
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.6f);
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }


}
