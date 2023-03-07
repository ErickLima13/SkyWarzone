using System.Collections;
using UnityEngine;

public class EnemyA : Base
{
    [Header("Enemy A")]
    public float pontoInicialCurva;
    public float pontoInicialCurvaX;

    public bool isCurva;

    public float grausCurva;
    private float rotacaoZ;

    private float incrementado;

    public float incrementar;

    public bool isLateral;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        if (!isLateral)
        {
            rotacaoZ = transform.eulerAngles.z;   
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }

    private void Shoot()
    {
        BaseShoot();
        shoot.GetComponent<Rigidbody2D>().velocity = -transform.up * shootSpeed;
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(Random.Range(delay[0], delay[1]));
        Shoot();
        StartCoroutine(ShootDelay());
    }

    private void Movement()
    {
        if (isLateral)
        {
            CurvaX();
        }
        else
        {
            CurvaY();
        }
    }

    private void CurvaY()
    {
        grausCurva = 90;

        if (transform.position.y <= pontoInicialCurva && !isCurva)
        {
            isCurva = true;
        }

        if (isCurva && incrementado < grausCurva)
        {
            rotacaoZ += incrementar;
            transform.rotation = Quaternion.Euler(0, 0, rotacaoZ);

            if (incrementar < 0)
            {
                incrementado += (incrementar * -1);
            }
            else
            {
                incrementado += incrementar;
            }

        }

        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void CurvaX()
    {
        grausCurva = 180;

        if (transform.position.x >= pontoInicialCurvaX && !isCurva)
        {
            isCurva = true;
        }

        if (isCurva && incrementado < grausCurva)
        {
            rotacaoZ += incrementar;
            transform.rotation = Quaternion.Euler(0, 0, rotacaoZ);

            if (incrementar < 0)
            {
                incrementado += (incrementar * -1);
            }
            else
            {
                incrementado += incrementar;
            }
        }

        transform.Translate(speed * Time.deltaTime * Vector3.down);
        isCurva = false;

    }

    private void OnBecameVisible()
    {
        StartCoroutine(ShootDelay());
    }
}
