using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float topLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float bottomLimit;
    [SerializeField] private float leftLimit;

    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private float timeBetweenShots;

    [SerializeField] private int defaultHealth;
    [SerializeField] private TextMeshProUGUI healthTextElement;

    [SerializeField] private ParticleSystem explosionEffect;

    private GameMode gameMode;
    private float timer;
    private int health;

    private bool isPlaying = false;

    private void Awake()
    {
        gameMode = FindFirstObjectByType<GameMode>();
        timer = timeBetweenShots;
        health = defaultHealth;
    }

    void Update()
    {
        if (isPlaying)
        {
            transform.Translate(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed * Time.deltaTime);
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
                Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
                transform.position.z
            );

            timer += Time.deltaTime;

            if (Input.GetKey(KeyCode.Space) && timer >= timeBetweenShots)
            {
                GameObject bulletClone = Instantiate(bullet, spawnPoint.transform.position, Quaternion.identity);
                timer = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int damage = 0;
        if (collision.CompareTag("EnemyBullet"))
        {
            damage = 10;
            Destroy(collision.gameObject);
            PlayExplosionEffect();
        }
        else if (collision.CompareTag("Enemy"))
        {
            damage = 20;
        }
        if (damage > 0)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                gameObject.SetActive(false);
                gameMode.StopGame();
            }
            healthTextElement.text = "Health: " + health;
        }
    }

    public void StartGame()
    {
        timer = timeBetweenShots;
        health = defaultHealth;
        healthTextElement.text = "Health: " + health;
        gameObject.SetActive(true);
        isPlaying = true;
    }

    private void PlayExplosionEffect()
    {
        ParticleSystem instance = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
    }
}
