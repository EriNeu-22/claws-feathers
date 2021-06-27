using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    private CanvasScaler canvas;
    public GameObject SceneLoaderMenu;

    public void Start()
    {
        canvas = GetComponent<CanvasScaler>();
    }

    public void QuitBackToMenu()
    {

        var sceneLoaderMenu = Instantiate(SceneLoaderMenu, new Vector3(canvas.referenceResolution.x / 2, canvas.referenceResolution.y / 2, 0), Quaternion.identity);
        sceneLoaderMenu.transform.SetParent(transform);


        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        StartCoroutine(DoFade("2_Menu"));

    }

    IEnumerator DoFade(string NextScene)
    {

        
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(NextScene);

    }


}
