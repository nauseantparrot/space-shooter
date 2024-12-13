using System.Collections;
using TMPro;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField] private GameObject gameTitleTextElement;
    [SerializeField] private GameObject gameOverTextElement;
    [SerializeField] private GameObject playButtonElement;
    [SerializeField] private GameObject levelTextElement;
    [SerializeField] private TextMeshProUGUI scoreTextElement;

    private Player player;
    private Spawner spawner;
    private int score = 0;
    private int level = 1;

    private void Awake()
    {
        player = FindFirstObjectByType<Player>();
        spawner = FindFirstObjectByType<Spawner>();
    }

    public void StartGame()
    {
        gameTitleTextElement.SetActive(false);
        gameOverTextElement.SetActive(false);
        playButtonElement.SetActive(false);
        levelTextElement.SetActive(true);

        score = 0;
        scoreTextElement.text = "Score: " + score;

        level = 1;
        levelTextElement.GetComponent<TextMeshProUGUI>().text = "Round " + level;

        player.StartGame();
        spawner.StartGame();
    }

    public void StopGame()
    {
        gameOverTextElement.SetActive(true);
        playButtonElement.SetActive(true);
        levelTextElement.SetActive(false);

        spawner.StopGame();

        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        foreach(Enemy enemy in enemies) {
            Destroy(enemy.gameObject);
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreTextElement.text = "Score: " + score;
        int newLevel = (score / 100) + 1;
        if (newLevel > level)
        {
            level = newLevel;
            levelTextElement.GetComponent<TextMeshProUGUI>().text = "Round " + level;
            spawner.IncreaseSpawnSpeed();
        }
    }
}
