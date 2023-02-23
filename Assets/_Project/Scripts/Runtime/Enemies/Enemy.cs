using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PlayerController playerController;

    public GameObject bulletPrefab;
    public Transform gunPos;

    public GameObject explosionPrefab;
    public GameObject[] loots;

    public float[] delay;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        StartCoroutine(ShootDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Shoot()
    {
        gunPos.right = playerController.transform.position - transform.position;
        GameObject temp = Instantiate(bulletPrefab, gunPos.position, gunPos.rotation);
        temp.GetComponent<Rigidbody2D>().velocity = gunPos.right * 3;
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(Random.Range(delay[0], delay[1]));
        Shoot();
        StartCoroutine(ShootDelay());
    }

    private void SpawnLoots()
    {
        int random = Random.Range(0, 100);

        if (random < 50)
        {
            random = Random.Range(0, 100);

            int id;
            if (random > 85)
            {
                id = 2;
            }
            else if (random > 50)
            {
                id = 1;
            }
            else
            {
                id = 0;
            }

            Instantiate(loots[id], transform.position, transform.localRotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "PlayerShoot": 

                Destroy(collision.gameObject);
                GameObject temp = Instantiate(explosionPrefab,transform.position,transform.localRotation);
                Destroy(temp,0.5f);
                SpawnLoots();
                Destroy(gameObject);
                break;
        }
    }
}
