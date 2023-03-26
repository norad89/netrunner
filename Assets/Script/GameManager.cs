using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> platformPrefabs; // list of platform prefabs
    public GameObject player; // player reference
    public float spawnRate = 1f; // platform spawn frequency
    private bool isGameActive = true; // active game flag
    private float speed = 23; // game speed

    void Start()
    {
        StartGame();
    }

    void Update() {
        player.transform.Translate(Vector3.right * Time.deltaTime * speed);
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
                
                Destroy(nextPlatform, 2f);
            }
        }
    }

    // Funzione per il controllo della fine del gioco
    public void GameOver()
    {
        isGameActive = false;
    }

    // Funzione per l'avvio del gioco
    public void StartGame()
    {
        // Start spawning platforms
        StartCoroutine(SpawnGround());   
        // Starts player movement
    }
}