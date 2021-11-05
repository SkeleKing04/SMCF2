using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag != "Player")
        {
            //Debug.Log("Collided at " + gameObject.transform.position);
            Destroy(gameObject);
        }
    }
}
