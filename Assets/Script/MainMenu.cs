using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject titleScreen;
    public GameObject difficultyScreen;
    public GameObject creditsScreen;

    public Button startGameButton;
    public Button difficultyButton;
    public Button creditsButton;

    void Start()
    {
        startGameButton.onClick.AddListener(StartGame);
        difficultyButton.onClick.AddListener(ShowDifficultyScreen);
        creditsButton.onClick.AddListener(ShowCreditsScreen);
    }

    void StartGame()
    {
        titleScreen.SetActive(false);
        gameManager.StartGame();
    }

    void ShowDifficultyScreen()
    {
        titleScreen.SetActive(false);
        difficultyScreen.SetActive(true);
    }

    void ShowCreditsScreen()
    {
        titleScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }
}