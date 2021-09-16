using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private BossAI bossAI;
    private PlayerHealth playerHealth;
    public enum GameState
    {
        Playing,
        Paused
    };
    public List<GameState> gameStateOrder;
    public enum MenuState{
        Off,
        MainMenu,
        InGame,
        Options,
        Paused,
        Win,
        GameOver
    };
    //public static MenuState menuState;
    public List<MenuState> menuOrder;
    private Canvas canvas;
    public GameObject[] Menus;
    public static float GlobalTimer;
    public Text[] currentMenu;
    // Start is called before the first frame update
    void Start()
    {
        bossAI = FindObjectOfType<BossAI>();
        canvas = FindObjectOfType<Canvas>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        //Menus = GameObject.FindGameObjectsWithTag("Menu");
        GlobalTimer = 0;
        OpenMainMenu();
        currentMenu[1].text = "";
        foreach(GameObject menus2 in Menus)
        {
            currentMenu[1].text += menus2.name + "\n";
        }
    }

    // Update is called once per frame
    void Update()
    {

        GlobalTimer += Time.deltaTime;
        if(Input.GetKeyUp(KeyCode.Escape) && gameStateOrder[gameStateOrder.Count - 1] != GameState.Paused)
        {
            PauseGame();
        }
        if(bossAI.dead)
        {
            bossAI.dead = false;
            Victory();
        }
        if(playerHealth.dead)
        {
            playerHealth.dead = false;  
            GameOver();
        }
    }
    private void FixedUpdate()
    {

    }
    private void SetUI()
    {
        currentMenu[0].text = "";
        for(int i = 0; i < gameStateOrder.Count; i++)
        {
            currentMenu[0].text += gameStateOrder[i].ToString() + "\n";
        }
        foreach(GameObject menu in Menus)
        {
            menu.SetActive(false);
        }
        switch(menuOrder[menuOrder.Count - 1])
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
        switch(gameStateOrder[gameStateOrder.Count - 1])
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
        Debug.Log("Opening Main Menu");
        menuOrder.Add(MenuState.MainMenu);
        gameStateOrder.Add(GameState.Paused);
        SetUI();
    }
    public void RestartToMainMenu()
    {
        Debug.Log("Restarting To Main Menu");
        menuOrder.Clear();
        gameStateOrder.Clear();
        OpenMainMenu();
    }
    public void StartGame()
    {
        Debug.Log("Starting Game");
        menuOrder.Add(MenuState.InGame);
        bossAI.resetBoss();
        gameStateOrder.Add(GameState.Playing);
        SetUI();
    }
    public void OpenOptions()
    {
        Debug.Log("Opening Options");
        menuOrder.Add(MenuState.Options);
        gameStateOrder.Add(GameState.Paused);
        SetUI();
    }
    public void PauseGame()
    {
        Debug.Log("Pausing");
        menuOrder.Add(MenuState.Paused);
        gameStateOrder.Add(GameState.Paused);
        SetUI();
    }
    public void Victory()
    {
        Debug.Log("Victory");
        menuOrder.Add(MenuState.Win);
        gameStateOrder.Add(GameState.Paused);
        SetUI();
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
        menuOrder.Add(MenuState.GameOver);
        gameStateOrder.Add(GameState.Paused);
        SetUI();
    }
    public void menuReturn()
    {
        Debug.Log("Returning to previous menu");
        menuOrder.Remove(menuOrder[menuOrder.Count - 1]);
        //Debug.Log("The count of gameStateOrder is " + gameStateOrder.Count + "\nThe gameState at the end is " + gameStateOrder[gameStateOrder.Count - 1]);
        gameStateOrder.Remove(gameStateOrder[gameStateOrder.Count - 1]);
        //Debug.Log("The gameState at the end is now " + gameStateOrder[gameStateOrder.Count - 1]);
        SetUI();
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quiting...");
    }
}
