using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> platformPrefabs;
    public GameObject player;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverScreen;
    public GameObject titleScreen;
    public Button restartButton;
    public float spawnRate = 1f;
    public bool isGameActive;
    private int score;
    private float speed = 23;

    void Start()
    {

    }

    void Update() {
        if(isGameActive) {
            player.transform.Translate(Vector3.right * Time.deltaTime * speed);
            UpdateScore();   
        }
    }

    // Funzione per il controllo della generazione dei target
    IEnumerator SpawnGround()
    {
                
        while (isGameActive)
        {
            // Attendi per un tempo casuale basato sulla frequenza di spawn
            yield return new WaitForSeconds(spawnRate);

            // Se il giocatore ha superato una certa distanza sull'asse y, genera un target a destra del giocatore
            if (player.transform.position.x > 45f)
            {
                // Scegli un prefab casuale dalla lista dei target
                int index = Random.Range(0, platformPrefabs.Count);

                Vector3 randomOffset = new Vector3(Random.Range(2.5f, 6.0f), Random.Range(-7.0f, -2), 0);
                // Calcola la posizione di spawn a destra del giocatore
                Vector3 spawnPos = new Vector3(player.transform.position.x, 0f, 0f) + new Vector3(20f, 0f, 0f) + randomOffset;

                // Genera il target a destra del giocatore
                GameObject nextPlatform = Instantiate(platformPrefabs[index], spawnPos, platformPrefabs[index].transform.rotation);
                
            }
        }
    }
    public void UpdateScore() {
        score = (int)player.transform.position.x;
        scoreText.text = "Distance: " + score;
    }

    public void GameOver() {
        gameOverScreen.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame() {
        Physics.gravity = new Vector3(0, -9.8f, 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnGround());
        titleScreen.gameObject.SetActive(false);
    }
}