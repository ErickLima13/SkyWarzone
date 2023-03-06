using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Base
{
    private Rigidbody2D playerRb;

    private Vector2 movementInputs;

    public GameObject bulletPrefab;

   

    private void Initialization()
    {
        playerRb = GetComponent<Rigidbody2D>();
        gameManager.playerController = this;
        gameManager.isAlive = true;
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Movement()
    {

        if (gameManager.isAlive)
        {
            movementInputs.x = Input.GetAxis("Horizontal");
            movementInputs.y = Input.GetAxis("Vertical");
            playerRb.velocity = movementInputs * speed;
        }
    }

    private void Shoot()
    {
        BaseShoot();
        temp.GetComponent<Rigidbody2D>().velocity = new(0, shootSpeed);
    }
}
