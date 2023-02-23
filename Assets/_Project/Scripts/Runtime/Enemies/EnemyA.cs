using System.Collections;
using UnityEngine;

public class EnemyA : Base
{
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

        rotacaoZ = transform.eulerAngles.z;

        if (isLateral)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            grausCurva = 180;
        }
        else
        {
            rotacaoZ = transform.eulerAngles.z;
            grausCurva = 50;
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
        temp.GetComponent<Rigidbody2D>().velocity = -transform.up * shootSpeed;
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootDelay);
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

        if (transform.position.y > 2)
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);

        }
    }

    private void CurvaY()
    {
        grausCurva = 50;

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
