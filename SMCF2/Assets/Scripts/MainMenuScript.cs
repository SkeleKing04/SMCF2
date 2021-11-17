using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Button[] menuButtons;
    public GameObject[] menus;
    void Start()
    {
        loadMenu(0);
    }
    public void loadMenu(int SelectedMenu)
    {
        foreach(GameObject menu in menus)
        {
            menu.SetActive(false);
        }
        /*switch(GameManager.playerBossDead[0])
        {
            case true:
                menus[3].SetActive(true);
                GameManager.playerBossDead[0] = false;
                break;
        }
        switch(GameManager.playerBossDead[1])
        {
            case true:
                menus[2].SetActive(true);
                GameManager.playerBossDead[1] = false;
                break;
        }*/
        menus[SelectedMenu].SetActive(true);
    }
    public void startFight(string boss)
    {
        SceneManager.LoadScene(boss);
    }
    // Start is called before the first frame update
}
