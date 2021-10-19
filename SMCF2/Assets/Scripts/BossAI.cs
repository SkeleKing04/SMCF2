using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
{
    private GameManager gameManager;
    public enum BossType
    {
        Bomb,
        Missle,
        Bullet
    };
    public BossType bossType;
    private int bossTypeAsInt;
    public GameObject[] bossTerrains;
    public Transform[] PlayerSpawns;
    public Transform[] BossSpawns;
    public GameObject[] BossModel;
    private GameObject player;
    public Image BossBarFull;
    public float StartHealth = 100;
    public float CurrentHealth;
    public bool dead = false;
    private float savedTime;
    public float LaunchForce;
    public ParticleSystem[] particle;
    private bool Launching;
    private Rigidbody target;
    private Vector3 lookPos;
    private Vector3 initialPos;
    private float rotateSpeed = 1000;
    public GameObject fireTransform;
    private SinDraw spawnPosSet;
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
        LowWideMoving,
        SkyFire
    };
    public ShootingArangement shootingArangement;
    private float rpm;
    public float bombTargetOffset;
    private Vector3 spawn;
    private Rigidbody shellInstance;
    private bool canSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        switch(bossType)
        {
            case BossType.Bomb:
                bossTypeAsInt = 0;
                break;
            case BossType.Missle:
                bossTypeAsInt = 1;
                break;
            case BossType.Bullet:
                bossTypeAsInt = 2;
                break;
        }
        //bossTerrains = GameObject.FindGameObjectsWithTag("Terrain");
        player = GameObject.FindGameObjectWithTag("Player");
        //rigidbody = GetComponent<Rigidbody>();
        spawnPosSet = GetComponent<SinDraw>();
        gameManager = FindObjectOfType<GameManager>();
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.gameStateOrder[gameManager.gameStateOrder.Count - 1] == GameManager.GameState.Playing)
        {
//            Debug.Log("Gate 1");
        if(GameManager.GlobalTimer - savedTime >= 0.5 && Launching)
        {
            target.AddForce(target.transform.up * (LaunchForce / 2), ForceMode.Impulse);
            target.AddForce(target.transform.forward * -LaunchForce, ForceMode.Impulse);
            particle[1].Play();
            Launching = false;
        }
                lookPos = player.gameObject.transform.position - gameObject.transform.position;
        lookPos.y = 0;
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotateSpeed);
        lookPos = player.gameObject.transform.position - gameObject.transform.position;
        //Quaternion test = gameObject.transform.rotation;
        //test.x += 90;
        //fireTransform.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotateSpeed);
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
            case ShootingArangement.SkyFire:
                rpm = 0.2f;
                spawnPosSet.Speed = 20;
                spawnPosSet.Size = 10;
                spawnPosSet.xMulti = 1;
                spawnPosSet.yMulti = 0;
                spawnPosSet.zMulti = 1;
                spawnPosSet.xOffset = player.transform.position.x;
                spawnPosSet.yOffset = player.transform.position.y + 50;
                spawnPosSet.zOffset = player.transform.position.z;
                spawnPosSet.SinCosTan[0] = 0;
                spawnPosSet.SinCosTan[1] = 0;
                spawnPosSet.SinCosTan[2] = 1;
                break;
        }
        if(canSpawn && GameManager.GlobalTimer - savedTime >= rpm)
        {
            switch (ammo)
            {
               case Ammo.bomb:
                    //Debug.Log("Spawned Bomb");
                    spawn = spawnPosSet.pos;
                    //Debug.Log("Rotation " + transform.rotation.y);
                    //lookPos = player.transform.position - spawn;
                    //Debug.Log("LookPos - " + lookPos);
                    //Quaternion.Slerp(rigidbody.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotateSpeed);
                    Rigidbody shellInstance = Instantiate(bullet[0], new Vector3(spawn.x + Random.Range(-bombTargetOffset - 1, bombTargetOffset),spawn.y + Random.Range(-bombTargetOffset - 1, bombTargetOffset),spawn.z + Random.Range(-bombTargetOffset - 1, bombTargetOffset)), fireTransform.transform.rotation) as Rigidbody;
                    //TRIG USED HERE
                    // Sqrt(y^2 + (Sqrt(x^2 + z^2))^2)
                    //shellInstance.velocity = new Vector3(((player.transform.position.x + Random.Range(-bombTargetOffset, bombTargetOffset))- shellInstance.transform.position.x) / 6.35f,((25 + Random.Range(-bombTargetOffset, bombTargetOffset))),((player.transform.position.z + Random.Range(-bombTargetOffset, bombTargetOffset) )- shellInstance.transform.position.z) / 6.35f);
                    shellInstance.velocity = 100 * -shellInstance.transform.up;
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
                    shellInstance = Instantiate(bullet[2], spawn, fireTransform.transform.rotation) as Rigidbody;
                    lookPos = player.transform.position - shellInstance.transform.position;
                    shellInstance.transform.rotation = Quaternion.Slerp(shellInstance.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * 10000);
                    shellInstance.velocity = shellInstance.transform.forward * 10;

                    //canSpawn = false;
                    break;
            }
            savedTime = GameManager.GlobalTimer;
        }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Bullet")
        {
            CurrentHealth -= Mathf.Clamp(1, 0, StartHealth * (bossTypeAsInt + 1));
            BossBarFull.fillAmount = CurrentHealth / (StartHealth * (bossTypeAsInt + 1));
        }
        if(CurrentHealth <= 0 && gameManager.gameStateOrder[gameManager.gameStateOrder.Count - 1] == GameManager.GameState.Playing)
        {
            Debug.Log("Gate 2");
            if(bossTypeAsInt >= 2)
            {
                dead = true;
                gameObject.SetActive(false);
            }
            else
            {
                bossTypeAsInt += Mathf.Clamp(1, 0, 2);
                StartFight();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player" && gameManager.gameStateOrder[gameManager.gameStateOrder.Count - 1] == GameManager.GameState.Playing)
        {
            Debug.Log("Gate 3");
            savedTime = GameManager.GlobalTimer;
            particle[0].Play();
            target = other.attachedRigidbody;
            Launching = true;
        }
    }
    private void StartFight()
    {
        for(int i = 0; i <= bossTerrains.Length - 1; i++)
        {
            bossTerrains[i].SetActive(false);
        }
        dead = false;
        gameObject.SetActive(true);
        CurrentHealth = (StartHealth * (bossTypeAsInt + 1));
        BossBarFull.fillAmount = CurrentHealth / (StartHealth * (bossTypeAsInt + 1));
        bossTerrains[bossTypeAsInt].SetActive(true);
        gameObject.transform.position = BossSpawns[bossTypeAsInt].position;
        player.transform.position = PlayerSpawns[bossTypeAsInt].position;
        switch(bossTypeAsInt)
        {
            case 0:
                bossType = BossType.Bomb;
                ammo = Ammo.bomb;
                shootingArangement = ShootingArangement.SkyFire;
                break;
                case 1:
                bossType = BossType.Missle;
                ammo = Ammo.missile;
                shootingArangement = ShootingArangement.LowWideMoving;
                break;
                case 2:
                bossType = BossType.Bullet;
                ammo = Ammo.bullet;
                shootingArangement = ShootingArangement.Gattling;
                break;
        }
        LoadBossModel(bossTypeAsInt);
    }
    public void LoadBossModel(int i)
    {
        foreach(GameObject model in BossModel)
        {
            model.SetActive(false);
        }
        BossModel[i].SetActive(true);
    }
    public void resetBoss()
    {
        bossTypeAsInt = 0;
        StartFight();
    }
}
