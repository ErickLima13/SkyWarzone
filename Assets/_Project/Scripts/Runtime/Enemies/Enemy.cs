using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Base
{
    //[Header("Enemy")]
    

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        StartCoroutine(ShootDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Shoot()
    {
        if (gameManager.isAlive)
        {
            gunPos.right = gameManager.playerController.transform.position - transform.position;
            BaseShoot();
            shoot.GetComponent<Rigidbody2D>().velocity = gunPos.right * shootSpeed;
        }
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(Random.Range(delay[0], delay[1]));
        Shoot();
        StartCoroutine(ShootDelay());
    }
}
