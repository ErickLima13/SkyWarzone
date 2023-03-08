using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TagBullets
{
    PlayerShoot,
    EnemyShoot,
}

public enum GameStates
{
    Intro,
    GamePlay
}

public class GameManager : MonoBehaviour
{
    public GameStates currentState;

    public bool takeOff;

    public float speedTakeOff;
    private float currentSpeed;

    public Color finalColor;
    public Color initColor;

    [Header("Settings Player")]
    public PlayerController playerController;
    public int lifes;
    public int score;
    public float invincibleTime;
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

    public Transform phase;
    [Range(0,10)] public float speedPhase;

    private void Initialization()
    {
        StartCoroutine(IntroPhase());
    }

    // Start is called before the first frame update
    private void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isAlive && currentState == GameStates.GamePlay)
        {
            MovementLimits();
        }

        if(takeOff && currentState == GameStates.Intro)
        {
            Vector3 target = new(0, -2, 0);
            playerController.transform.position = Vector3.MoveTowards(playerController.transform.position, target, currentSpeed * Time.deltaTime);

            if(playerController.transform.position == target)
            {
                StartCoroutine(MoveUp());
                currentState= GameStates.GamePlay;
            }

            playerController.planeGas.color = Color.Lerp(initColor, finalColor, 0.2f);
        }
    }

    private void LateUpdate()
    {
        if(currentState == GameStates.GamePlay)
        {
            MovePhase();
        }
        
    }

    private void MovePhase()
    {
        Vector3 target = new(phase.position.x, -33, phase.position.z);
        phase.position = Vector3.MoveTowards(phase.position, target, speedPhase * Time.deltaTime);
    }
    public void AddScore(int value)
    {
        score += value;
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
            StartCoroutine(InstantiatePLayer());
        }
        else
        {
            print("Game Over");
        }
    }

    private IEnumerator InstantiatePLayer()
    {
        yield return new WaitForSeconds(2);
        Instantiate(playerPrefab, spawnPlayer.position, Quaternion.identity);

        yield return new WaitForEndOfFrame();
        playerController.StartCoroutine(playerController.Invincible());
    }

    private IEnumerator IntroPhase()
    {
        playerController.planeGas.color = initColor;
        playerController.transform.localScale = new(0.5f, 0.5f, 0);
        playerController.transform.position = new Vector3(0f, -5.3f, 0);

        yield return new WaitForSeconds(2);
        takeOff = true;

        for(currentSpeed = 0; currentSpeed < speedTakeOff; currentSpeed += 0.2f)
        {
            yield return new WaitForSeconds(0.2f);

        }
    }

    private IEnumerator MoveUp()
    {
        for(float s = playerController.transform.localScale.x; s < 1; s += 0.006f)
        {
            playerController.transform.localScale = new Vector3(s,s,s);
            playerController.planeGas.color = Color.Lerp(playerController.planeGas.color, finalColor, 0.2f);
            yield return new WaitForEndOfFrame();
        }
    }
}
