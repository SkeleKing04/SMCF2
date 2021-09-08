using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHeath : MonoBehaviour
{
    public Image BossBarFull;
    public float StartHealth = 100;
    public float CurrentHealth;
    public bool defeated = false;
    public bool dead = false;
    private float savedTime;
    public Text text;
    public bool textOn = false;
    private void Start()
    {
        CurrentHealth = StartHealth;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Bullet")
        {
            CurrentHealth -= Mathf.Clamp(1, 0, StartHealth);
            BossBarFull.fillAmount = CurrentHealth / 100;
        }
    }
    private void Update()
    {
        switch (CurrentHealth)
        {
            case 0:
                defeated = true;
                break;
        }
        if(dead && (GameManager.GlobalTimer - savedTime >= 0.5))
        {
            if(textOn)
            {
                savedTime = GameManager.GlobalTimer;
                text.gameObject.SetActive(false);
            }
            else if (!textOn)
            {
                savedTime = GameManager.GlobalTimer;
                text.gameObject.SetActive(true);
            }
        }
    }
    public void NextBoss()
    {
        defeated = false;
        CurrentHealth = StartHealth;
    }
}
