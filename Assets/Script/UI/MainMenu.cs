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
    public GameObject playerSelectScreen;

    public Button startGameButton;
    public Button selectDifficultyButton;
    public Button creditsButton;
    public Button easyDifficultyButton;
    public Button mediumDifficultyButton;
    public Button hardDifficultyButton;
    public Button cubePlayer;
    public Button spherePlayer;

    void Start()
    {
        startGameButton.onClick.AddListener(ShowPlayerSelectScreen);
        selectDifficultyButton.onClick.AddListener(ShowDifficultyScreen);
        creditsButton.onClick.AddListener(ShowCreditsScreen);
        easyDifficultyButton.onClick.AddListener(() => SetDifficulty(1));
        mediumDifficultyButton.onClick.AddListener(() => SetDifficulty(2));
        hardDifficultyButton.onClick.AddListener(() => SetDifficulty(3));
        GameManager.Instance.LoadHighScore();
    }

    public void StartGame(string playerType)
    {
        titleScreen.SetActive(false);
        SceneManager.LoadScene(1);
        GameManager.Instance.StartGame(playerType);
        UIMainScene.Instance.ToggleGameplayUI();
    }

    public void ShowPlayerSelectScreen() {
         titleScreen.SetActive(false);
         playerSelectScreen.SetActive(true);
         cubePlayer.onClick.AddListener(() => StartGame("cube"));
         spherePlayer.onClick.AddListener(() => StartGame("sphere"));
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