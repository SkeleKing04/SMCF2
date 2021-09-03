using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public float savedTime;
    public GameObject fireTransform;
    public Rigidbody[] bullet;
    public enum Ammo
    {
        bomb,
        missile,
        bullet
    };
    public Ammo ammo;
    public enum ShootingArangement{
        Gattling,
        Starfire,
        Controlled
    };
    public ShootingArangement shootingArangement;
    public int bulletCount;
    public Vector3 spawn;
    private Vector3 lookPos;
    private Rigidbody shellInstance;
    private GameObject player;
    public float Speed;
    public float Size;
    private bool canSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawn && GameManager.GlobalTimer - savedTime >= 0.01)
        {
            for(int i = 0; i < bulletCount; i++)
            {
            switch (ammo)
            {
               case Ammo.bomb:
                    //Debug.Log("Spawned Bomb");
                    spawn = new Vector3((Mathf.Sin(Time.time * Speed) * Size), fireTransform.transform.position.y, (Mathf.Cos(Time.time * Speed) * Size));
                    Debug.Log("Rotation " + transform.rotation.y);
                    //lookPos = player.transform.position - spawn;
                    //Debug.Log("LookPos - " + lookPos);
                    //Quaternion.Slerp(rigidbody.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotateSpeed);
                    Rigidbody shellInstance = Instantiate(bullet[0], spawn, fireTransform.transform.rotation) as Rigidbody;
                    //TRIG USED HERE
                    // Sqrt(y^2 + (Sqrt(x^2 + z^2))^2)
                    shellInstance.velocity = new Vector3
                                                    (
                                                        ((player.transform.position.x + Random.Range(-5, 5))- shellInstance.transform.position.x),
                                                        ((player.transform.position.y + Random.Range(-5, 5) )- shellInstance.transform.position.y) / 2,
                                                        ((player.transform.position.z + Random.Range(-5, 5) )- shellInstance.transform.position.z)
                    );
                    //canSpawn = false;
                    break;
                case Ammo.missile:
                    Debug.Log("Spawned Missile");
                    spawn = new Vector3(fireTransform.transform.position.x + i, fireTransform.transform.position.y, fireTransform.transform.position.z);
                    shellInstance = Instantiate(bullet[1], spawn, fireTransform.transform.rotation) as Rigidbody;
                    shellInstance.velocity = 1 * fireTransform.transform.forward;
                    canSpawn = false;
                    break;
                case Ammo.bullet:
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
    /*public void Fire(int AmmoType){
               switch(shootingArangement){
            case ShootingArangement.Gattling:
                
        }
    }*/
}
