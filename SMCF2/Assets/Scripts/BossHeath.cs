using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHeath : MonoBehaviour
{
    public Image BossBarFull;
    public float StartHealth = 100;
    public float CurrentHealth;
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
                Destroy(gameObject);
                break;
        }
    }
}