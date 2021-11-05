using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]private BossAI bossAI;
    private PlayerHealth playerHealth;
    public enum MenuState
    {
        empty,
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
    [SerializeField] private AudioClip[] BGM;
    // Start is called before the first frame update
    void Start()
    {
        //bossAI = FindObjectOfType<BossAI>();
        canvas = FindObjectOfType<Canvas>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        //Menus = GameObject.FindGameObjectsWithTag("Menu");
        GlobalTimer = 0;
        OpenMainMenu();
        //currentMenu[1].text = "";
        foreach(GameObject menus2 in Menus)
        {
            //currentMenu[1].text += menus2.name + "\n";
        }
        gameObject.GetComponent<AudioSource>().clip = BGM[Random.Range(0, BGM.Length)];
    }

    // Update is called once per frame
    void Update()
    {

        GlobalTimer += Time.deltaTime;
        if(Input.GetKeyUp(KeyCode.Escape) && Time.timeScale != 0)
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
    public void LoadNextScene(string nextBossScene)
    {
        SceneManager.LoadScene(nextBossScene);
    }
    private void SetUI(MenuState menu)
    {
        if(menu != MenuState.empty)
        {
            menuOrder.Add(menu);
        }
        //currentMenu[0].text = "";
        //for(int i = 0; i < gameStateOrder.Count; i++)
        //{
        //    currentMenu[0].text += gameStateOrder[i].ToString() + "\n";
        //}
        foreach(GameObject menus in Menus)
        {
            menus.SetActive(false);
        }
        switch(menuOrder[menuOrder.Count - 1])
        {
            case MenuState.Off:
                break;
            case MenuState.MainMenu:
                Menus[0].SetActive(true);
                Time.timeScale = 0;
                break;
            case MenuState.InGame:
                Menus[1].SetActive(true);
                Time.timeScale = 1;
                break;
            case MenuState.Options:
                Menus[2].SetActive(true);
                Time.timeScale = 0;
                break;
            case MenuState.Paused:
                Menus[3].SetActive(true);
                Time.timeScale = 0;
                break;
            case MenuState.Win:
                Menus[4].SetActive(true);
                Time.timeScale = 0;
                break;
            case MenuState.GameOver:
                Menus[5].SetActive(true);
                Time.timeScale = 0;
                break;
        }
    }
    public void OpenMainMenu()
    {
        Debug.Log("Opening Main Menu");
        SetUI(MenuState.MainMenu);
    }
    public void RestartToMainMenu()
    {
        Debug.Log("Restarting To Main Menu");
        menuOrder.Clear();
        OpenMainMenu();
    }
    public void StartGame()
    {
        Debug.Log("Starting Game");
        bossAI.StartFight();
        SetUI(MenuState.InGame);
    }
    public void OpenOptions()
    {
        Debug.Log("Opening Options");
        SetUI(MenuState.Options);
    }
    public void PauseGame()
    {
        Debug.Log("Pausing");
        SetUI(MenuState.Paused);
    }
    public void Victory()
    {
        Debug.Log("Victory");
        SetUI(MenuState.Win);
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
        SetUI(MenuState.GameOver);
    }
    public void menuReturn()
    {
        Debug.Log("Returning to previous menu");
        menuOrder.Remove(menuOrder[menuOrder.Count - 1]);
        //Debug.Log("The count of gameStateOrder is " + gameStateOrder.Count + "\nThe gameState at the end is " + gameStateOrder[gameStateOrder.Count - 1]);
        //Debug.Log("The gameState at the end is now " + gameStateOrder[gameStateOrder.Count - 1]);
        SetUI(MenuState.empty);
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quiting...");
    }
}
