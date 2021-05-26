using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    void Start()
    {
        string name = SceneManager.GetActiveScene().name;
        Debug.Log("Nome da cena: " + name);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
