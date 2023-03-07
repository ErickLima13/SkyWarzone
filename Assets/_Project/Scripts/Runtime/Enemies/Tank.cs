using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Base
{
    //[Header("Tank")]

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnBecameVisible()
    {
        StartCoroutine(ShootDelay());
    }

    private void OnBecameInvisible()
    {
        StopAllCoroutines();
    }

    private void Shoot()
    {
        if (gameManager.isAlive)
        {
            gunPos.up = gameManager.playerController.transform.position - transform.position;
            BaseShoot();
            shoot.GetComponent<Rigidbody2D>().velocity = gunPos.up * shootSpeed;
        }
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(Random.Range(delay[0], delay[1]));
        Shoot();
        StartCoroutine(ShootDelay());
    }
}
