using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // functions the same a bullet but insted ingores boss and enemy bullets collision
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag != "Boss" && collision.transform.tag != "Enemy Bullet")
        {
            Destroy(gameObject);
        }
    }
}
