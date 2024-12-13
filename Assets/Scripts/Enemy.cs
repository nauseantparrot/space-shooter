using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int defaultHealth = 10;

    [SerializeField] private float speed;

    [SerializeField] private int prize = 10;

    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private float timeBetweenShots;

    [SerializeField] private ParticleSystem explosionEffect;

    private GameMode gameMode;
    private int health;

    private void Awake()
    {
        gameMode = FindFirstObjectByType<GameMode>();
        health = defaultHealth;
    }

    void Start()
    {
        StartCoroutine(Shoot());
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int damage = 0;
        if (collision.CompareTag("Bullet"))
        {
            damage = 10;
            Destroy(collision.gameObject);
            PlayExplosionEffect();
        }
        else if (collision.CompareTag("Player"))
        {
            damage = 20;
        }
        if (damage > 0)
        {
            health -= damage;
            if (health <= 0)
            {
                gameMode.IncreaseScore(prize);
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShots);
            foreach (GameObject spawnPoint in spawnPoints)
            {
                Instantiate(bullet, spawnPoint.transform.position, Quaternion.identity);
            }
        }
    }

    private void PlayExplosionEffect()
    {
        ParticleSystem instance = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
    }
}
