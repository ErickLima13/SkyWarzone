using System.Collections;
using UnityEngine;

public class PlayerController : Base
{
    [Header("Player")]
    private Rigidbody2D playerRb;

    private Vector2 movementInputs;

    private SpriteRenderer playerSr;

    public Color invincibleColor;

    public SpriteRenderer planeGas;

    private void Initialization()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerSr = GetComponent<SpriteRenderer>();
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
        if (gameManager.isAlive && gameManager.currentState == GameStates.GamePlay)
        {
            Movement();

            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
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
        BaseShoot();
        shoot.GetComponent<Rigidbody2D>().velocity = new(0, shootSpeed);
    }

    public IEnumerator Invincible()
    {
        Collider2D collider2D = GetComponent<Collider2D>();
        collider2D.enabled = false;
        playerSr.color = invincibleColor;
        StartCoroutine(Blink());

        yield return new WaitForSeconds(gameManager.invincibleTime);
        collider2D.enabled = true;
        playerSr.color = Color.white;
        playerSr.enabled = true;
        StopAllCoroutines();
    }

    private IEnumerator Blink()
    {
        yield return new WaitForSeconds(0.3f);
        playerSr.enabled = !playerSr.enabled;
        StartCoroutine(Blink());
    }
}
