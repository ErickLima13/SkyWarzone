using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;

    private Vector2 movementInputs;

    [Range(0, 50)] public float speed;
    [Range(0, 50)] public float shootSpeed;

    public Transform gunPos;

    public GameObject bulletPrefab;

    private void Initialization()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
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
    }

    private void Shoot()
    {
        GameObject temp = Instantiate(bulletPrefab);
        temp.transform.position = gunPos.position;
        temp.GetComponent<Rigidbody2D>().velocity = new(0, shootSpeed);
    }
}
