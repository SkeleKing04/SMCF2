using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public float savedTime;
    public SinDraw spawnPosSet;
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
        LowWideMoving
    };
    public ShootingArangement shootingArangement;
    public float rpm;
    public float bombTargetOffset;
    public Vector3 spawn;
    private Vector3 lookPos;
    private Rigidbody shellInstance;
    private GameObject player;
    private bool canSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosSet = GetComponent<SinDraw>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch (shootingArangement)
        {
            case ShootingArangement.Gattling:
                rpm = 0.1f;
                spawnPosSet.Speed = 1;
                spawnPosSet.Size = 2;
                spawnPosSet.xMulti = 1;
                spawnPosSet.yMulti = 0;
                spawnPosSet.zMulti = 1;
                spawnPosSet.xOffset = 0;
                spawnPosSet.yOffset = transform.position.y + 5;
                spawnPosSet.zOffset = 0;
                spawnPosSet.SinCosTan[0] = 0;
                spawnPosSet.SinCosTan[1] = 0;
                spawnPosSet.SinCosTan[2] = 1;
            break;
            case ShootingArangement.Starfire:
                rpm = 0.5f;
                spawnPosSet.Speed = 5;
                spawnPosSet.Size = 10;
                spawnPosSet.xMulti = 1;
                spawnPosSet.yMulti = 0;
                spawnPosSet.zMulti = 1;
                spawnPosSet.xOffset = 0;
                spawnPosSet.yOffset = transform.position.y + 10;
                spawnPosSet.zOffset = 0;
                spawnPosSet.SinCosTan[0] = 0;
                spawnPosSet.SinCosTan[1] = 0;
                spawnPosSet.SinCosTan[2] = 1;
            break;
            case ShootingArangement.LowWideMoving:
                rpm = 0.4f;
                spawnPosSet.Speed = 2;
                spawnPosSet.Size = 5;
                spawnPosSet.xMulti = 5;
                spawnPosSet.yMulti = 1;
                spawnPosSet.zMulti = 0;
                spawnPosSet.xOffset = Mathf.Sin(Time.time * 1) * 10;
                spawnPosSet.yOffset = transform.position.y + 10;
                spawnPosSet.zOffset = Mathf.Cos(Time.time * 1) * 10;
                spawnPosSet.SinCosTan[0] = 0;
                spawnPosSet.SinCosTan[1] = 1;
                spawnPosSet.SinCosTan[2] = 0;
                break;
        }
        if(canSpawn && GameManager.GlobalTimer - savedTime >= rpm)
        {
            switch (ammo)
            {
               case Ammo.bomb:
                    //Debug.Log("Spawned Bomb");
                    spawn = spawnPosSet.pos;
                    Debug.Log("Rotation " + transform.rotation.y);
                    //lookPos = player.transform.position - spawn;
                    //Debug.Log("LookPos - " + lookPos);
                    //Quaternion.Slerp(rigidbody.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotateSpeed);
                    Rigidbody shellInstance = Instantiate(bullet[0], spawn, fireTransform.transform.rotation) as Rigidbody;
                    //TRIG USED HERE
                    // Sqrt(y^2 + (Sqrt(x^2 + z^2))^2)
                    shellInstance.velocity = new Vector3
                                                    (
                                                        ((player.transform.position.x + Random.Range(-bombTargetOffset, bombTargetOffset))- shellInstance.transform.position.x) / 6.35f,
                                                        ((25 + Random.Range(-bombTargetOffset, bombTargetOffset))),
                                                        ((player.transform.position.z + Random.Range(-bombTargetOffset, bombTargetOffset) )- shellInstance.transform.position.z) / 6.35f
                    );

                    //canSpawn = false;
                    break;
                case Ammo.missile:
                    Debug.Log("Spawned Missile");
                    spawn = spawnPosSet.pos;
                    shellInstance = Instantiate(bullet[1], spawn, fireTransform.transform.rotation) as Rigidbody;
                    shellInstance.velocity = 1 * fireTransform.transform.forward;
                    //canSpawn = false;
                    break;
                case Ammo.bullet:
                    Debug.Log("Spawned Bullet");
                    spawn = spawnPosSet.pos;
                    lookPos = player.transform.position - fireTransform.transform.position;
                    shellInstance = Instantiate(bullet[2], spawn, Quaternion.LookRotation(lookPos)) as Rigidbody;
                    shellInstance.velocity = (player.transform.position - shellInstance.transform.position) * 5;
                    lookPos = player.transform.position - shellInstance.transform.position;
                    shellInstance.transform.rotation = Quaternion.Slerp(shellInstance.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * 10000);
                    //canSpawn = false;
                    break;
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
