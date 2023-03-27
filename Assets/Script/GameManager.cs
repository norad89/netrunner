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
    public TextMeshProUGUI scoreText;
    public GameObject gameOverScreen;
    public GameObject titleScreen;
    public Button restartButton;
    public float platformSpawnRate = 1f;
    public float powerUpSpawnRate = 15f;
    public bool isGameActive;
    private int score;
    private float speed = 23;

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnPlatforms());
        StartCoroutine(SpawnPowerUps());
        titleScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isGameActive)
        {
            player.transform.Translate(Vector3.right * Time.deltaTime * speed);
            UpdateScore();
        }
    }

    IEnumerator SpawnPlatforms()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(platformSpawnRate);

            // Check distance reached from first platform
            if (player.transform.position.x > 45f)
            {
                // Choose a random groundprefab
                int index = Random.Range(0, platformPrefabs.Count);

                // Create a random offset for platform spawning and spawn platforms to the right of the player
                Vector3 randomOffset = new Vector3(Random.Range(2.5f, 6.0f), Random.Range(-7.0f, -2), 0);
                Vector3 spawnPos = new Vector3(player.transform.position.x, 0f, 0f) + new Vector3(20f, 0f, 0f) + randomOffset;
                GameObject nextPlatform = Instantiate(platformPrefabs[index], spawnPos, platformPrefabs[index].transform.rotation);
            }
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