using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float savedTime;
    private void Awake()
    {
        savedTime = GameManager.GlobalTimer;
    }
    void Update()
    {
        switch (GameManager.GlobalTimer >= savedTime + 30)
        {
            case true:
                Destroy(gameObject);
                break;
        }    
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag != "Boss" && collision.transform.tag != "Enemy Bullet")
        {
            //Debug.Log("Collided at " + gameObject.transform.position);
            Destroy(gameObject);
        }
    }
}
