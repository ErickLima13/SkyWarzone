using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController playerController;

    private Camera cam;

    [Header("Settings Player")]
    public Transform limitUp;
    public Transform limitDown;
    public Transform limitLeft;
    public Transform limitRight;

    [Header("Settings Cam")]
    public Transform limitCamLeft;
    public Transform limitCamRight;
    public Transform limitCamUp;
    public float speedCam;
    public float speedPhase;

    private void Initialization()
    {
        playerController = FindObjectOfType<PlayerController>();
        cam = Camera.main;
    }

    // Start is called before the first frame update
    private void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerMovementLimits();
    }

    private void LateUpdate()
    {
        ControlCam();
        
    }

    private void ControlCam()
    {
        if(cam.transform.position.x > limitCamLeft.position.x && cam.transform.position.x < limitCamRight.position.x)
        {
            MoveCam();
        }
        else if(cam.transform.position.x <= limitCamLeft.position.x && playerController.transform.position.x > limitCamLeft.position.x)
        {
            MoveCam();
        }
        else if(cam.transform.position.x >= limitCamRight.position.x && playerController.transform.position.x < limitCamRight.position.x)
        {
            MoveCam();
        }
    }

    private void MoveCam()
    {
        Vector3 targetCam = new(playerController.transform.position.x, cam.transform.position.y, cam.transform.position.z);
        cam.transform.position = Vector3.Lerp(cam.transform.position, targetCam, speedCam * Time.deltaTime);
    }

    private void MoveCamUp()
    {
        Vector3 targetCam = new(cam.transform.position.x,limitCamUp.position.y,cam.transform.position.z);
        cam.transform.position = Vector3.MoveTowards(cam.transform.position,targetCam,speedPhase* Time.deltaTime);
    }

    private void PlayerMovementLimits()
    {
        float posY = playerController.transform.position.y;
        float posX = playerController.transform.position.x;
        
        if(posY > limitUp.position.y)
        {
            playerController.transform.position = new Vector3(posX, limitUp.transform.position.y, 0);
        }
        else if(posY < limitDown.position.y)
        {
            playerController.transform.position = new Vector3(posX,limitDown.transform.position.y, 0);
        }

        if(posX > limitRight.position.x)
        {
            playerController.transform.position = new Vector3(limitRight.position.x,posY, 0);
        }
        else if(posX < limitLeft.position.x)
        {
            playerController.transform.position = new Vector3(limitLeft.position.x, posY, 0);
        }
    }
}
