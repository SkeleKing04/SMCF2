using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public float savedTime;
    public GameObject fireTransform;
    public Rigidbody[] bullet;
    public enum BulletState
    {
        bomb,
        missile,
        bullet
    };
    public BulletState bulletState;
    public int bulletCount;
    public Vector3 spawn;
    private Rigidbody shellInstance;
    private GameObject player;
    private bool canSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawn && GameManager.GlobalTimer - savedTime >= 0.1)
        {
            for(int i = 0; i < bulletCount; i++)
            {
            switch (bulletState)
            {
               case BulletState.bomb:
                    //Debug.Log("Spawned Bomb");
                    Rigidbody shellInstance = Instantiate(bullet[0], new Vector3(20 * Mathf.Cos(transform.rotation.y + (90* (Mathf.PI/180))), fireTransform.transform.position.y, 0 * Mathf.Sin(transform.rotation.y * 4)), fireTransform.transform.rotation) as Rigidbody;
                    //TRIG USED HERE
                    // Sqrt(y^2 + (Sqrt(x^2 + z^2))^2)
                    shellInstance.velocity = new Vector3
                                                    (
                                                        (player.transform.position.x - fireTransform.transform.position.x),
                                                        (player.transform.position.y - fireTransform.transform.position.y) / 2,
                                                        (player.transform.position.z - fireTransform.transform.position.z)
                    );
                    //canSpawn = false;
                    break;
                case BulletState.missile:
                    Debug.Log("Spawned Missile");
                    spawn = new Vector3(fireTransform.transform.position.x + i, fireTransform.transform.position.y, fireTransform.transform.position.z);
                    shellInstance = Instantiate(bullet[1], spawn, fireTransform.transform.rotation) as Rigidbody;
                    shellInstance.velocity = 1 * fireTransform.transform.forward;
                    canSpawn = false;
                    break;
                case BulletState.bullet:
                    Debug.Log("Spawned Bullet");
                    spawn = new Vector3(fireTransform.transform.position.x, fireTransform.transform.position.y, fireTransform.transform.position.z);
                    shellInstance = Instantiate(bullet[2], spawn, fireTransform.transform.rotation) as Rigidbody;
                    shellInstance.velocity = 1 * fireTransform.transform.forward;
                    canSpawn = false;
                    break;
            }
            }
            savedTime = GameManager.GlobalTimer;
        }
    }
}
