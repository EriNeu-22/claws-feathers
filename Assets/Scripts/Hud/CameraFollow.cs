using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ItemFollow
{
    public Transform item;
    public float maxLimit;
    public float minLimit;
    public float moveFactor;
}

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float maxLimitVertical;
    public float minLimitVertical;
    public float maxLimit;
    public float minLimit;

    private Vector3 cameraPos;
    private Vector3 lastCameraPos;

    public GameObject hud;

    void Start()
    {
        cameraPos = new Vector3(0, 0, -10);
        lastCameraPos = new Vector3(0, 0, -10);

    }
    void LateUpdate()
    {

        cameraPos.x = target.position.x;
        cameraPos.y = target.position.y;

        if (cameraPos != lastCameraPos)
        {

            cameraPos.x = Mathf.Clamp(cameraPos.x, minLimit, maxLimit);
            cameraPos.y = Mathf.Clamp(cameraPos.y, minLimitVertical, maxLimitVertical);

            transform.position = cameraPos;
            lastCameraPos = cameraPos;

            Vector3 hudPosition = hud.transform.position;

            hudPosition.x = cameraPos.x;
            hudPosition.y = cameraPos.y;

            hud.transform.position = hudPosition;

        }


    }




}
