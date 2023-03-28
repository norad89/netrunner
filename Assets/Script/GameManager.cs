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
    public float platformSpawnRate = 1f;
    public float powerUpSpawnRate = 15f;
    public float speed = 23f;
    public float initialSpawnPositionX = 60f;
    public bool isGameActive;
    public int powerUpCount = 3;
    private bool isFirstSpawn = true;
    private int score;

    public void StartGame()
    {
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnPlatforms());
        StartCoroutine(SpawnPowerUps());
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

    IEnumerator SpawnPlatforms()
    {
        // Initial platform spawn position
        Vector3 previousPlatformPosition = new Vector3(initialSpawnPositionX, 0f, 0f);

        while (isGameActive)
        {
            yield return new WaitForSeconds(platformSpawnRate);

            int index;
            // Choose a random platform unless it's first spawn
            if (isFirstSpawn)
            {
                index = 2;
                isFirstSpawn = false;
            }
            else
            {
                index = Random.Range(0, platformPrefabs.Count);
            }

            // Get next platform size
            float prefabSizeX = platformPrefabs[index].GetComponent<Renderer>().bounds.size.x;
            // Creates random offset
            Vector3 randomOffset = new Vector3(Random.Range(5.0f, 15.0f), Random.Range(-12.0f, -6.0f), 0);
            // Define actual spawn position based on previous platform position
            Vector3 spawnPos = previousPlatformPosition + new Vector3(prefabSizeX / 2, 0f, 0f) + randomOffset;
            // Instantiate new platform
            GameObject nextPlatform = Instantiate(platformPrefabs[index], spawnPos, platformPrefabs[index].transform.rotation);
            // Update previous platform position based on the size of the platform that just spawned  
            previousPlatformPosition = new Vector3(nextPlatform.transform.position.x + nextPlatform.GetComponent<Renderer>().bounds.size.x / 2, 0f, 0f);
        }

    }

    IEnumerator SpawnPowerUps()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(powerUpSpawnRate);

            // Check distance reached from first platform
            if (player.transform.position.x > 45f)
            {
                // Create a random offset for platform spawning and spawn platforms to the right of the player
                Vector3 randomOffset = new Vector3(Random.Range(2.5f, 6.0f), Random.Range(-7.0f, -2), 0);
                Vector3 spawnPos = new Vector3(player.transform.position.x, 0f, 0f) + new Vector3(20f, 6f, 0f) + randomOffset;
                GameObject powerUp = Instantiate(powerUpsPrefabs[0], spawnPos, powerUpsPrefabs[0].transform.rotation);
            }
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