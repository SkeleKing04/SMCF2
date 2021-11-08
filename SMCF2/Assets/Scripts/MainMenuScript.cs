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
        menus[SelectedMenu].SetActive(true);
    }
    public void startFight(string boss)
    {
        SceneManager.LoadScene(boss);
    }
    // Start is called before the first frame update
}
