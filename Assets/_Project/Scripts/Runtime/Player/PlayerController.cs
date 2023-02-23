using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Base
{
    private Rigidbody2D playerRb;

    private Vector2 movementInputs;

    public GameObject bulletPrefab;

    [Header("Settings limit")]
    public Transform limitUp;
    public Transform limitDown;
    public Transform limitLeft;
    public Transform limitRight;

    private void Initialization()
    {
        playerRb = GetComponent<Rigidbody2D>();
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
        movementInputs.x = Input.GetAxis("Horizontal");
        movementInputs.y = Input.GetAxis("Vertical");

        playerRb.velocity = movementInputs * speed;

        MovementLimits();
    }

    private void MovementLimits()
    {
        float posY = transform.position.y;
        float posX = transform.position.x;

        if (posY > limitUp.position.y)
        {
            transform.position = new Vector3(posX, limitUp.transform.position.y, 0);
        }
        else if (posY < limitDown.position.y)
        {
            transform.position = new Vector3(posX, limitDown.transform.position.y, 0);
        }

        if (posX > limitRight.position.x)
        {
            transform.position = new Vector3(limitRight.position.x, posY, 0);
        }
        else if (posX < limitLeft.position.x)
        {
            transform.position = new Vector3(limitLeft.position.x, posY, 0);
        }
    }

    private void Shoot()
    {
        BaseShoot();
        temp.GetComponent<Rigidbody2D>().velocity = new(0, shootSpeed);
    }
}
