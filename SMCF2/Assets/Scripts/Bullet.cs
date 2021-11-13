using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // when colliding with anything, (excluding the player)
        // die
        if(collision.transform.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
