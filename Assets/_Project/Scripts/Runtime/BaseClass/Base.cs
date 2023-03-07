using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class Base : MonoBehaviour
{
    [Header("Base")]
    public TagBullets TagBullet;
    public int idBullet;

    [Range(0, 50)] public float speed;
    [Range(0, 50)] public float shootSpeed;
    public float[] delay;

    public Transform gunPos;

    public GameManager gameManager;

    public GameObject shoot;



    // Start is called before the first frame update
    public virtual void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void BaseShoot()
    {
        shoot = Instantiate(gameManager.bulletsPrefabs[idBullet], gunPos.position, gunPos.localRotation);
        shoot.tag = TagBullet.ToString();
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

            Instantiate(gameManager.loots[id], transform.position, transform.localRotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float timer = 0.6f;
    

        switch (collision.gameObject.tag)
        {
            case "PlayerShoot":
                Destroy(collision.gameObject);
                GameObject temp = Instantiate(gameManager.explosionPrefab, transform.position, gameManager.explosionPrefab.transform.localRotation);
                temp.transform.parent = gameManager.phase;
                Destroy(temp, timer);
                SpawnLoots();
                Destroy(gameObject);
                break;
            case "EnemyShoot":
                if (TagBullet == TagBullets.EnemyShoot)
                {
                    return;
                }

                Destroy(collision.gameObject);
                GameObject explosion = Instantiate(gameManager.explosionPrefab, transform.position, gameManager.explosionPrefab.transform.localRotation);
                explosion.transform.parent = gameManager.phase;
                Destroy(explosion, timer);
                gameManager.PlayerHit();
                break;
        }
    }
}
