using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Base
{
    private PlayerController playerController;

    public float[] delay;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        playerController = gameManager.playerController;
        StartCoroutine(ShootDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Shoot()
    {
        gunPos.right = playerController.transform.position - transform.position;
        BaseShoot();
        temp.GetComponent<Rigidbody2D>().velocity = gunPos.right * 3;
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(Random.Range(delay[0], delay[1]));
        Shoot();
        StartCoroutine(ShootDelay());
    }
}
