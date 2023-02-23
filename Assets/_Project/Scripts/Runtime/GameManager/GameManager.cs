using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController playerController;

    [Header("Prefabs")]
    public GameObject[] bulletsPrefabs;

    private void Initialization()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Start is called before the first frame update
    private void Awake()
    {
        Initialization();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
