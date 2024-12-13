using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject[] spawnableObjects;
    [SerializeField] private float timeBetweenSpawns;

    private Coroutine coroutine;
    private float currentTimeBetweenSpawns;

    IEnumerator SpawnObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentTimeBetweenSpawns);

            int randomIndex = Random.Range(0, spawnPoints.Length);
            GameObject spawnPoint = spawnPoints[randomIndex];

            float randomValue = Random.Range(0f, 10f);

            int objectIndex;
            if (randomValue <= 5f)
            {
                objectIndex = 0;
            }
            else if (randomValue <= 8f)
            {
                objectIndex = 1;
            }
            else
            {
                objectIndex = 2;
            }

            Instantiate(spawnableObjects[objectIndex], spawnPoint.transform.position, Quaternion.identity);
        }
    }

    public void StartGame()
    {
        currentTimeBetweenSpawns = timeBetweenSpawns;
        coroutine = StartCoroutine(SpawnObject());
    }

    public void StopGame() {
        StopCoroutine(coroutine);
    }

    public void IncreaseSpawnSpeed()
    {
        currentTimeBetweenSpawns /= 1.1f;
    }
}
