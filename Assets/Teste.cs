using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{

    public GameObject aaaa;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= 100; i++)
        {
            aaaa = new GameObject();
        }
    }

    // Update is called once per frame
    void Update()
    {
        aaaa.transform.Rotate(0, .2f, 0);
    }
}
