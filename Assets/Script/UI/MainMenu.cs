using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject difficultyScreen;
    public GameObject creditsScreen;

    public Button startGameButton;
    public Button selectDifficultyButton;
    public Button creditsButton;
    public Button easyDifficultyButton;
    public Button mediumDifficultyButton;
    public Button hardDifficultyButton;

    void Start()
    {
        startGameButton.onClick.AddListener(StartGame);
        selectDifficultyButton.onClick.AddListener(ShowDifficultyScreen);
        creditsButton.onClick.AddListener(ShowCreditsScreen);
        easyDifficultyButton.onClick.AddListener(() => SetDifficulty(1));
        mediumDifficultyButton.onClick.AddListener(() => SetDifficulty(2));
        hardDifficultyButton.onClick.AddListener(() => SetDifficulty(3));
        GameManager.Instance.LoadHighScore();
    }

    public void StartGame()
    {
        titleScreen.SetActive(false);
        SceneManager.LoadScene(1);
        GameManager.Instance.StartGame();
        UIMainScene.Instance.ToggleGameplayUI();
    }

    public void ShowDifficultyScreen()
    {
        titleScreen.SetActive(false);
        difficultyScreen.SetActive(true);
    }

    public void SetDifficulty(int difficulty)
    {
        difficultyScreen.SetActive(false);
        titleScreen.SetActive(true);
        GameManager.Instance.difficulty = difficulty;
    }

    public void ShowCreditsScreen()
    {
        //titleScreen.SetActive(false);
        //creditsScreen.SetActive(true);
    }
}