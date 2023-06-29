using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject> platformPrefabs;
    public List<GameObject> powerUpsPrefabs;
    public GameObject player;
    public GameObject startingPlatform;
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
    private bool isSpawningStarted = false;
    public bool gameOverBool;
    private int score;
    private int oldHighScore = 0;
    public int newHighScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        powerUpCount = 3;
        score = 0;
        UpdateDifficulty(difficulty);
        isGameActive = true;
        isSpawningStarted = true;
    }

    void Update()
    {
        if (isGameActive && player)
        {
            score = (int)player.transform.position.x;
            UIMainScene.Instance.UpdateDifficultyText(difficulty);
            UIMainScene.Instance.UpdateScore(score);
            UIMainScene.Instance.UpdatePowerUpCount(powerUpCount);
            player.transform.Translate(Vector3.right * Time.deltaTime * speed);
            if (player.transform.position.y < -8 && !gameOverBool)
            {
                GameOver();
            }
        }

        if (isSpawningStarted)
        {
            StartCoroutine(SpawnManager());
            isSpawningStarted = false;
        }
    }

    IEnumerator SpawnManager()
    {
        // Initial platform spawn position
        Vector3 previousPlatformPosition = new Vector3(initialSpawnPositionX, 0f, 0f);

        if (gameOverBool) {
            gameOverBool = false;
        }

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
                break;

            case 2:
                minSpawnOffsetX = 5f;
                maxSpawnOffsetX = 15f;
                powerUpSpawnRate = 10;
                platformSpawnRate = 0.8f;
                speed = 23;
                break;

            case 3:
                minSpawnOffsetX = 6f;
                maxSpawnOffsetX = 18f;
                powerUpSpawnRate = 5;
                platformSpawnRate = 0.6f;
                speed = 30;
                break;
        }
    }

    [System.Serializable]
    public class SaveData
    {
        public int HighScore;
    }

    public void SaveHighScore()
    {
        string fileName = "savefile.json";
        string path = Path.Combine(Application.persistentDataPath, fileName);

        // Creare un nuovo oggetto SaveData con l'high score corrente
        SaveData data = new SaveData();
        data.HighScore = score;

        // Convertire l'oggetto SaveData in formato JSON
        string json = JsonUtility.ToJson(data);

        // Salvare il file JSON nel percorso persistente specifico per la build WebGL
        // StartCoroutine(SaveFile(path, json));
        SaveFile(path, json);
    }

    private void SaveFile(string path, string json)
    {
        // byte[] bytes = Encoding.UTF8.GetBytes(json);

        if (File.Exists(path)) {
            File.WriteAllText(path, json);
        }
        

        // // Creare una richiesta HTTP POST per salvare il file
        // byte[] bytes = Encoding.UTF8.GetBytes(json);
        // Debug.Log(bytes);
        // UnityWebRequest www = UnityWebRequest.Post(path, json);

        // // Impostare il contenuto del messaggio come JSON
        // www.SetRequestHeader("Content-Type", "application/json");

        // // Allegare i dati JSON alla richiesta
        // www.uploadHandler = new UploadHandlerRaw(bytes);

        // // Attendere la fine della richiesta
        // yield return www.SendWebRequest();

        // if (www.result != UnityWebRequest.Result.Success)
        // {
        //     // Gestire l'errore di salvataggio del file
        //     Debug.LogError("Errore durante il salvataggio del file: " + www.error);
        // }
    }

    public void LoadHighScore()
    {
        string fileName = "savefile.json";
        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(path)) {
            // Caricare il file JSON dal percorso persistente specifico per la build WebGL
            LoadFile(path);
        } else {
            File.Create(path);
        }
    }

    private void LoadFile(string path)
    {
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            // Controllare se l'high score caricato è maggiore di quello attuale
            if (data.HighScore > score)
            {
                // Se l'high score caricato è maggiore, assegnarlo a score
                oldHighScore = data.HighScore;
                UIMainScene.Instance.UpdateHighScore(data.HighScore);
            }
        }
        // // Creare una richiesta HTTP GET per caricare il file
        // UnityWebRequest www = UnityWebRequest.Get(path);

        // // Attendere la fine della richiesta
        // yield return www.SendWebRequest();

        // if (www.result == UnityWebRequest.Result.Success)
        // {
        //     // Convertire il contenuto del file in una stringa JSON
        //     string json = www.downloadHandler.text;
        //     if (!string.IsNullOrEmpty(json)) {
        //         // Convertire il JSON in un oggetto SaveData
        //         SaveData data = JsonUtility.FromJson<SaveData>(json);

        //         // Controllare se l'high score caricato è maggiore di quello attuale
        //         if (data.HighScore > score)
        //         {
        //             // Se l'high score caricato è maggiore, assegnarlo a score
        //             oldHighScore = data.HighScore;
        //             UIMainScene.Instance.UpdateHighScore(data.HighScore);
        //         }
        //     }
        // }
        // else
        // {
        //     // Gestire l'errore di caricamento del file
        //     Debug.LogError("Errore durante il caricamento del file: " + www.error);
        // }
    }

    public void GameOver()
    {
        gameOverBool = true;
        newHighScore = score;
        isGameActive = false;
        isSpawningStarted = false;
        UIMainScene.Instance.ShowGameOverScreen();
        if (newHighScore > oldHighScore)
        {
            SaveHighScore();
            UIMainScene.Instance.UpdateHighScore(newHighScore);
            oldHighScore = score;
        }
    }
}