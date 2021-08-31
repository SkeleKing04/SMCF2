using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float savedTime;
    private void Awake()
    {
        savedTime = GameManager.GlobalTimer;
    }
    void Update()
    {
        switch (GameManager.GlobalTimer >= savedTime + 3)
        {
            case true:
                Destroy(gameObject);
                break;
        }    
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag != "Player" || collision.transform.tag != "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
