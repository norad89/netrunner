using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMainScene : MonoBehaviour
{
    public static UIMainScene Instance { get; private set; }

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerUpText;
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverScreen;
    public string playerType;

    public Button restartButton;
    public Button backToMenuButton;

    public int powerUpCount = 3;

    public int difficulty;

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

    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        backToMenuButton.onClick.AddListener(BackToMenu);
    }

    public void ToggleGameplayUI()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        bool showUI = currentScene.buildIndex == 0;

        powerUpText.gameObject.SetActive(showUI);
        difficultyText.gameObject.SetActive(showUI);
        scoreText.gameObject.SetActive(showUI);
    }

    public void ShowGameOverScreen(string type)
    {
        playerType = type;
        gameOverScreen.gameObject.SetActive(true);
        restartButton.onClick.AddListener(RestartGame);
        GameManager.Instance.isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        Physics.gravity = new Vector3(0, -9.8f, 0);
        gameOverScreen.gameObject.SetActive(false);
        GameManager.Instance.StartGame(playerType);
    }

    public void BackToMenu()
    {
        gameOverScreen.gameObject.SetActive(false);
        Physics.gravity = new Vector3(0, -9.8f, 0);
        SceneManager.LoadScene(0);
        ToggleGameplayUI();
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Distance: " + score;
    }

    public void UpdatePowerUpCount(int powerUpCount)
    {
        if (GameManager.Instance.playerType == "cube")
        {
            powerUpText.text = "Double Jumps: " + powerUpCount;
        }
        else
        {
            powerUpText.text = "Air Dashes: " + powerUpCount;
        }
    }

    public void UpdateHighScore(int highScore)
    {
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    public void UpdateDifficultyText(int difficulty)
    {
        switch (difficulty)
        {
            case 1:

                difficultyText.text = "Difficulty: Easy";
                break;

            case 2:

                difficultyText.text = "Difficulty: Medium";
                break;

            case 3:

                difficultyText.text = "Difficulty: Hard";
                break;
        }
    }
}