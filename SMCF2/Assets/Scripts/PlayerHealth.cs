using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image HealthBarFull;
    public float StartHealth = 100;
    public float CurrentHealth;
    private void Start()
    {
        CurrentHealth = StartHealth;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy Bullet")
        {
            CurrentHealth -= Mathf.Clamp(1, 0, StartHealth);
            HealthBarFull.fillAmount = CurrentHealth / 100;
        }
        HealthBarFull.fillAmount = CurrentHealth / 100;
        switch (CurrentHealth)
        {
            case 0:
                GameManager.playerBossDead[0] = true;
                break;
        }
    }
}