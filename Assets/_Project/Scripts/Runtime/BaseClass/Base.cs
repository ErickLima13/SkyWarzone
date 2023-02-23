using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public int idBullet;

    [Range(0, 50)] public float speed;
    [Range(0, 50)] public float shootSpeed;
    [Range(0, 50)] public float shootDelay;

    public Transform gunPos;

    public GameManager gameManager;

    public GameObject temp;

    // Start is called before the first frame update
    public virtual void Start()
    {
        gameManager = FindObjectOfType<GameManager>();  
    }

    public void BaseShoot() 
    {
        temp = Instantiate(gameManager.bulletsPrefabs[idBullet], gunPos.position, transform.rotation);
    }
}
