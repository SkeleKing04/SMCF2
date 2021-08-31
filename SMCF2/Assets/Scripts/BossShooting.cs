using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public float savedTime;
    public Transform fireTransform;
    public Rigidbody bullet;
    public int bulletCount;
    private bool canSpawn = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.GlobalTimer > 1 && canSpawn)
        {
            //Debug.Log("Started Spawning");
            //savedTime = GameManager.GlobalTimer;
            for(int i = 0; i < bulletCount; i++)
            {
                //Debug.Log("I is - " + i + "\nGlobal Timer is " + GameManager.GlobalTimer + "\nAttempting to spawn");
                //if(GameManager.GlobalTimer > savedTime + 0.2)
                //{
                    Debug.Log("Spawned Missile");
                    Vector3 spawn = new Vector3(fireTransform.transform.position.x + i * 2, fireTransform.transform.position.y, fireTransform.transform.position.z);
                    Rigidbody shellInstance = Instantiate(bullet, spawn, fireTransform.transform.rotation) as Rigidbody;
                    shellInstance.velocity = 1 * fireTransform.up;
                    //savedTime = GameManager.GlobalTimer;
                //}

            }
            canSpawn = false;
        }
    }
}
