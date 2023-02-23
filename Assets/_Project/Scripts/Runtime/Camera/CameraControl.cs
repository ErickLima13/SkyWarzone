using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private GameManager gameManager;

    [Header("Settings Cam")]
    public Transform limitCamLeft;
    public Transform limitCamRight;
    public Transform limitCamUp;
    public float speedCam;
    public float speedPhase;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void ControlCam()
    {
        if (Camera.main.transform.position.x > limitCamLeft.position.x && Camera.main.transform.position.x < limitCamRight.position.x)
        {
            MoveCam();
        }
        else if (Camera.main.transform.position.x <= limitCamLeft.position.x && gameManager.playerController.transform.position.x > limitCamLeft.position.x)
        {
            MoveCam();
        }
        else if (Camera.main.transform.position.x >= limitCamRight.position.x && gameManager.playerController.transform.position.x < limitCamRight.position.x)
        {
            MoveCam();
        }
    }

    private void MoveCam()
    {
        Vector3 targetCam = new(gameManager.playerController.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetCam, speedCam * Time.deltaTime);
    }

    private void MoveCamUp()
    {
        Vector3 targetCam = new(Camera.main.transform.position.x, limitCamUp.position.y, Camera.main.transform.position.z);
        Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, targetCam, speedPhase * Time.deltaTime);
    }   
}
