using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private BossAI bossAI;
    public enum GameState
    {
        Playing,
        Paused
    };
    public GameState gameState;
    public enum MenuState{
        Off,
        MainMenu,
        InGame,
        Options,
        Paused,
        Win,
        GameOver
    };
    public MenuState menuState;
    private Canvas canvas;
    public GameObject[] Menus;
    public static float GlobalTimer;
    // Start is called before the first frame update
    void Start()
    {
        bossAI = FindObjectOfType<BossAI>();
        canvas = FindObjectOfType<Canvas>();
        Menus = GameObject.FindGameObjectsWithTag("Menu");
        GlobalTimer = 0;
        SetUI();
    }

    // Update is called once per frame
    void Update()
    {
        GlobalTimer += Time.deltaTime;
    }
    private void FixedUpdate()
    {

    }
    public void SetUI()
    {
        foreach(GameObject menu in Menus)
        {
            menu.SetActive(false);
        }
        switch(menuState)
        {
            case MenuState.Off:
                break;
            case MenuState.MainMenu:
                Menus[0].SetActive(true);
                break;
            case MenuState.InGame:
                Menus[1].SetActive(true);
                break;
            case MenuState.Options:
                Menus[2].SetActive(true);
                break;
            case MenuState.Paused:
                Menus[3].SetActive(true);
                break;
            case MenuState.Win:
                Menus[4].SetActive(true);
                break;
            case MenuState.GameOver:
                Menus[5].SetActive(true);
                break;
        }
        switch(gameState)
        {
            case GameState.Playing:
            Time.timeScale = 1;
            break;
            case GameState.Paused:
            Time.timeScale = 0;
            break;
        }
    }
    public void OpenMainMenu()
    {
        menuState = MenuState.MainMenu;
        gameState = GameState.Paused;
        SetUI();
    }
    public void StartGame()
    {
        menuState = MenuState.InGame;
        gameState = GameState.Playing;
        SetUI();
    }
    public void OpenOptions()
    {
        menuState = MenuState.Options;
        gameState = GameState.Paused;
        SetUI();
    }
    public void PauseGame()
    {
        menuState = MenuState.Paused;
        gameState = GameState.Paused;
        SetUI();
    }
    public void Victory()
    {
        menuState = MenuState.Win;
        gameState = GameState.Paused;
        SetUI();
    }
    public void GameOver()
    {
        menuState = MenuState.GameOver;
        gameState = GameState.Paused;
        SetUI();
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quiting...");
    }
}
