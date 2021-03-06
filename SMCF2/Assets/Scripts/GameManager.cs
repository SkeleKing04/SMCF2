using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // a list of the order that the menus have been in
    // used for returning to a previous menu
    public List<int> menuOrder;
    // the menus in the scene
    public GameObject[] Menus;
    // the global timer
    // used to time or set times for various functions
    public static float GlobalTimer;
    // MUSIC BB
    [SerializeField] private AudioClip[] BGM;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        // resets the timer
        GlobalTimer = 0;
        // loads the menu in menus[0]
        //SetUI(0);
        // loads a random song
        int rand = Random.Range(0, BGM.Length);
        gameObject.GetComponent<AudioSource>().clip = BGM[rand];
    }
    void Update()
    {
        //updates the timer
        GlobalTimer += Time.deltaTime;
        // if player presses escape, open the pause screen
        if(Input.GetKeyUp(KeyCode.Escape) && Time.timeScale != 0)
        {
            SetUI(2);
        }
    }
    // load the scene
    public void LoadNextScene(string nextBossScene)
    {
        SceneManager.LoadScene(nextBossScene);
    }
    public void SetUI(int menu)
    {
        // disable all the menus
        foreach(GameObject menus in Menus)
        {
            menus.SetActive(false);
        }
        // add this menu to be load to the menu order
        menuOrder.Add(menu);
        // enable that menu
        Menus[menu].SetActive(true);
        // if the menu is the hud, run in normal time
        if (Menus[menu].name == "InGameHUD")
        {
            Time.timeScale = 1;
        }
        else
        {
            // pause the game
            Time.timeScale = 0;
        }
    }
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void startFight(string boss)
    {
        SceneManager.LoadScene(boss);
    }
    public void menuReturn()
    {
        Debug.Log("Returning to previous menu");
        menuOrder.RemoveAt(menuOrder.Count - 1);
        SetUI(menuOrder[menuOrder.Count - 1]);
        menuOrder.RemoveAt(menuOrder.Count - 1);
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quiting...");
    }
}
