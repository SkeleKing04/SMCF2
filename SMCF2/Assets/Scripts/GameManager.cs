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
    public List<int> menuOrder;
    private Canvas canvas;
    public GameObject[] Menus;
    public static float GlobalTimer;
    public Text[] currentMenu;
    [SerializeField] private AudioClip[] BGM;
    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        GlobalTimer = 0;
        SetUI(0);
        int rand = Random.Range(0, BGM.Length);
        Debug.Log("Random number - " + rand);
        gameObject.GetComponent<AudioSource>().clip = BGM[rand];
    }
    // Update is called once per frame
    void Update()
    {

        GlobalTimer += Time.deltaTime;
        if(Input.GetKeyUp(KeyCode.Escape) && Time.timeScale != 0)
        {
            SetUI(2);
        }
        if(bossAI.dead)
        {
            bossAI.dead = false;
            SetUI(3);
        }
        if(playerHealth.dead)
        {
            playerHealth.dead = false;  
            SetUI(4);
        }
    }
    public void LoadNextScene(string nextBossScene)
    {
        SceneManager.LoadScene(nextBossScene);
    }
    private void SetUI(int menu)
    {
        menuOrder.Add(menu);
        foreach(GameObject menus in Menus)
        {
            menus.SetActive(false);
        }
        Menus[menu].SetActive(true);
        if (menu == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
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
