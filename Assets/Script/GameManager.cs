using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> platformPrefabs;
    public List<GameObject> powerUpsPrefabs;
    public GameObject player;
    public GameObject startingPlatform;
    public GameObject gameOverScreen;
    public Button restartButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerUpText;
    public TextMeshProUGUI difficultyText;
    public float platformSpawnRate = 0.8f;
    public float speed = 23f;
    public float initialSpawnPositionX = 60f;
    public float minSpawnOffsetX = 5f;
    public float maxSpawnOffsetX = 15f;
    public bool isGameActive;
    public int powerUpCount = 3;
    public int difficulty = 2;
    public int powerUpSpawnRate = 10;
    private bool isFirstSpawn = true;
    private int score;

    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnManager());
    }

    void Update()
    {
        if (isGameActive)
        {
            player.transform.Translate(Vector3.right * Time.deltaTime * speed);
            UpdateScore();
            UpdatePowerUpCount(powerUpCount);
            if (player.transform.position.y < -8)
            {
                GameOver();
            }
        }
    }

    IEnumerator SpawnManager()
    {
        // Initial platform spawn position
        Vector3 previousPlatformPosition = new Vector3(initialSpawnPositionX, 0f, 0f);

        while (isGameActive)
        {
            yield return new WaitForSeconds(platformSpawnRate);

            int index;
            int powerUpSpawnChance;
            // Choose a random platform unless it's first spawn
            if (isFirstSpawn)
            {
                powerUpSpawnChance = 100;
                index = 2;
                isFirstSpawn = false;
            }
            else
            {
                index = Random.Range(0, platformPrefabs.Count);
                powerUpSpawnChance = Random.Range(1, 100);
            }

            GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
            int platformCount = platforms.Length;
            if (platformCount < 5)
            {
                // Get next platform size
                float prefabSizeX = platformPrefabs[index].GetComponent<Renderer>().bounds.size.x;
                // Creates random offset
                Vector3 randomOffset = new Vector3(Random.Range(minSpawnOffsetX, maxSpawnOffsetX), Random.Range(-12.0f, -6.0f), 0);
                // Define actual spawn position based on previous platform position
                Vector3 spawnPos = previousPlatformPosition + new Vector3(prefabSizeX / 2, 0f, 0f) + randomOffset;
                // Instantiate new platform
                GameObject nextPlatform = Instantiate(platformPrefabs[index], spawnPos, platformPrefabs[index].transform.rotation);
                // Update previous platform position based on the size of the platform that just spawned  
                previousPlatformPosition = new Vector3(nextPlatform.transform.position.x + nextPlatform.GetComponent<Renderer>().bounds.size.x / 2, 0f, 0f);

                if (powerUpSpawnChance <= powerUpSpawnRate)
                {
                    Vector3 randomPowerUpOffset = new Vector3(Random.Range(minSpawnOffsetX, maxSpawnOffsetX), Random.Range(15.0f, 11.0f), 0);
                    GameObject powerUp = Instantiate(powerUpsPrefabs[0], spawnPos + randomPowerUpOffset, powerUpsPrefabs[0].transform.rotation);
                }
            }
        }
    }

    public void UpdateDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            case 1:
                minSpawnOffsetX = 4f;
                maxSpawnOffsetX = 12f;
                powerUpSpawnRate = 20;
                platformSpawnRate = 1f;
                speed = 20;
                difficultyText.text = "Difficulty: Easy";
                break;

            case 2:
                minSpawnOffsetX = 5f;
                maxSpawnOffsetX = 15f;
                powerUpSpawnRate = 10;
                platformSpawnRate = 0.8f;
                speed = 23;
                difficultyText.text = "Difficulty: Medium";
                break;

            case 3:
                minSpawnOffsetX = 6f;
                maxSpawnOffsetX = 18f;
                powerUpSpawnRate = 5;
                platformSpawnRate = 0.6f;
                speed = 30;
                difficultyText.text = "Difficulty: Hard";
                break;
        }
    }

    public void UpdateScore()
    {
        score = (int)player.transform.position.x;
        scoreText.text = "Distance: " + score;
    }

    public void UpdatePowerUpCount(int powerUpCount)
    {
        powerUpText.text = "Double Jumps: " + powerUpCount;
    }

    public void GameOver()
    {
        gameOverScreen.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        Physics.gravity = new Vector3(0, -9.8f, 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}