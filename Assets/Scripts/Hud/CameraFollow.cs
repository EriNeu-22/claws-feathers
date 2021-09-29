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
    public Transform targetAux;
    public float maxLimitVertical;
    public float minLimitVertical;
    public float maxLimit;
    public float minLimit;

    private Vector3 cameraPos;
    private Vector3 lastCameraPos;

    public GameObject hud;
    public GameObject player;

    void Start()
    {
        cameraPos = new Vector3(0, 0, -10);
        lastCameraPos = new Vector3(0, 0, -10);

    }

    private bool BackCamera = false;
    private float TIME_TO_BACK_CAMERA = 1f;


    void LateUpdate()
    {

        if (CutsceneCameraMoveAtHomeTitle.Equals("CutsceneCameraMoveAtHomeTitle"))
        {

            if (!BackCamera)
            {
                float posX = target.transform.position.x + Time.deltaTime * 6;
                float posY = 0.498f * posX + 4.715f;

                if (posX >= 23.5f)
                {
                    posY = target.transform.position.y;
                    posX = target.transform.position.x;
                    TIME_TO_BACK_CAMERA -= Time.deltaTime;

                    if(TIME_TO_BACK_CAMERA <= 0)
                    {
                        BackCamera = true;
                        TIME_TO_BACK_CAMERA = 1f;
                    }
                    
                }

                Vector3 currentPosition = new Vector3(posX, posY, 0);
                target.transform.position = currentPosition;

            } else
            {
                float posX = target.transform.position.x - Time.deltaTime * 6;

                float posY = 0.498f * posX + 4.715f;
                Vector3 currentPosition = new Vector3(posX, posY, 0);
                target.transform.position = currentPosition;

                if(posX <= -5f)
                {
                    currentPosition = new Vector3(posX, posY, 0);
                    target.transform.position = currentPosition;
                    CutsceneCameraMoveAtHomeTitle = "";
                    target = player.gameObject.transform;
                    player.SendMessage("SecondCutsceneIsOver");
                    minLimitVertical = 2;
                    minLimit = -5;
}
            }

        }

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

    private string CutsceneCameraMoveAtHomeTitle = "";
    
    public void CutsceneCameraMoveAtHome(GameObject gameObject)
    {

        targetAux = target;
        target = gameObject.transform;

        target.position = cameraPos;
        CutsceneCameraMoveAtHomeTitle = "CutsceneCameraMoveAtHomeTitle";

    }


}
