using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TagBullets
{
    PlayerShoot,
    EnemyShoot,
}

public class GameManager : MonoBehaviour
{
    [Header("Settings Player")]
    public PlayerController playerController;
    public int lifes;
    public bool isAlive;
    public Transform spawnPlayer;
    public Transform limitUp;
    public Transform limitDown;
    public Transform limitLeft;
    public Transform limitRight;

    [Header("Prefabs")]
    public GameObject[] bulletsPrefabs;
    public GameObject[] loots;
    public GameObject explosionPrefab;
    public GameObject playerPrefab;

    private void Initialization()
    {
        
    }

    // Start is called before the first frame update
    private void Awake()
    {
        Initialization();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isAlive)
        {
            MovementLimits();
        }
    }


    private void MovementLimits()
    {
        float posY = playerController.transform.position.y;
        float posX = playerController.transform.position.x;

        if (posY > limitUp.position.y)
        {
            playerController.transform.position = new Vector3(posX, limitUp.position.y, 0);
        }
        else if (posY < limitDown.position.y)
        {
            playerController.transform.position = new Vector3(posX, limitDown.position.y, 0);
        }

        if (posX > limitRight.position.x)
        {
            playerController.transform.position = new Vector3(limitRight.position.x, posY, 0);
        }
        else if (posX < limitLeft.position.x)
        {
            playerController.transform.position = new Vector3(limitLeft.position.x, posY, 0);
        }
    }

    public void PlayerHit()
    {
        isAlive = false;
        Destroy(playerController.gameObject);
        lifes--;

        if(lifes >= 0)
        {
            Instantiate(playerPrefab,spawnPlayer.position, Quaternion.identity);
        }
        else
        {
            print("Game Over");
        }
    }
}
