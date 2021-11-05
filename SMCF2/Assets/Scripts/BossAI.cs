using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
{
    // Gamemanger in the scene
    private GameManager gameManager;
    // The type of boss the player is currently fighting
    public enum BossType
    {
        Bomb,
        Missle,
        Bullet
    };
    public BossType bossType;
    //public int bossTypeAsInt;
    //public GameObject[] bossTerrains;
    // Points at which the player spawns
    public Transform PlayerSpawn;
    // Points at which the boss spawns
    //public Transform[] BossSpawns;
    // Model that the boss uses
    // Relates to bossType
    //public GameObject[] BossModel;
    // The player
    private GameObject player;
    // The Boss' health bar
    public Image BossBarFull;
    // Maxium health of the boss
    public float StartHealth = 100;
    // Current health of the boss
    public float CurrentHealth;
    // For when the boss is killed
    public bool dead = false;
    // Used for repeating effects or for waiting
    public float[] savedTime;
    // Force that the player is launched with when they get too close
    public float LaunchForce;
    // Particle effects
    public ParticleSystem[] particle;
    // Check for launching the player
    private bool Launching;
    // Object to launch
    private Rigidbody target;
    // Where to look (the player)
    private Vector3 lookPos;
    //private Vector3 initialPos;
    // Speed that the boss rotates to lookPos
    private float rotateSpeed = 1000;
    // Where projectiles are spawned from
    public GameObject[] fireTransform;
    // RNG BABY!!!!!
    private int rand;
    // For dynamic bullet spawns
    //private SinDraw spawnPosSet;
    // The different projectiles as prefabs
    public Rigidbody[] bullet;
    // The type of ammo used the boss
    public enum Ammo
    {
        bomb,
        missile,
        bullet
    };
    public Ammo ammo;
    // How frequently the projectiles are spawned
    public float rpm;
    // A small offset so the projectile paths aren't the same every time
    //public float bombTargetOffset;
    // Where the bullets spawn
    //private Vector3 spawn;
    // Where the bullets spawn from
    ///private Rigidbody shellInstance;
    // If projectiles can be spawned
    private bool canSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        /*switch(bossType)
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
        }*/
        //bossTerrains = GameObject.FindGameObjectsWithTag("Terrain");
        // Finds the player in the scene
        player = GameObject.FindGameObjectWithTag("Player");
        //rigidbody = GetComponent<Rigidbody>();
        // Finds the scripts
        //spawnPosSet = GetComponent<SinDraw>();
        gameManager = FindObjectOfType<GameManager>();
        // Disables the boss in the scene
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale != 0)
        {
            //Debug.Log("Gate 1");

        lookPos = player.gameObject.transform.position - gameObject.transform.position;
        lookPos.y = 0;
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotateSpeed);
        lookPos = player.gameObject.transform.position - gameObject.transform.position;
        //Quaternion test = gameObject.transform.rotation;
        //test.x += 90;
        //fireTransform.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotateSpeed);
        if(GameManager.GlobalTimer - savedTime[0] >= 0.5 && Launching)
        {
            target.AddForce(target.transform.up * (LaunchForce / 2), ForceMode.Impulse);
            target.AddForce(target.transform.forward * -LaunchForce, ForceMode.Impulse);
            particle[1].Play();
            Launching = false;
        }
        if(canSpawn && GameManager.GlobalTimer - savedTime[1] >= rpm)
        {
            switch (ammo)
            {
               case Ammo.bomb:
                    //Debug.Log("Spawned Bomb");
                    //spawn = spawnPosSet.pos;
                    //Debug.Log("Rotation " + transform.rotation.y);
                    //lookPos = player.transform.position - spawn;
                    //Debug.Log("LookPos - " + lookPos);
                    //Quaternion.Slerp(rigidbody.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotateSpeed);
                    rand = UnityEngine.Random.Range(0,2);
                    Rigidbody shellInstance = Instantiate(bullet[0], fireTransform[rand].transform.position, fireTransform[rand].transform.rotation) as Rigidbody;
                    //TRIG USED HERE
                    // Sqrt(y^2 + (Sqrt(x^2 + z^2))^2)
                    //shellInstance.velocity = new Vector3(((player.transform.position.x + Random.Range(-bombTargetOffset, bombTargetOffset))- shellInstance.transform.position.x) / 6.35f,((25 + Random.Range(-bombTargetOffset, bombTargetOffset))),((player.transform.position.z + Random.Range(-bombTargetOffset, bombTargetOffset) )- shellInstance.transform.position.z) / 6.35f);
                    shellInstance.velocity = 25 * -shellInstance.transform.forward;
                    //canSpawn = false;
                    break;
                case Ammo.missile:
                    Debug.Log("Spawned Missile");
                    //spawn = spawnPosSet.pos;
                    rand = UnityEngine.Random.Range(2,13);
                    shellInstance = Instantiate(bullet[1], fireTransform[rand].transform.position, fireTransform[rand].transform.rotation) as Rigidbody;
                    shellInstance.velocity = 10 * fireTransform[rand].transform.forward;
                    //canSpawn = false;
                    break;
                case Ammo.bullet:
                    Debug.Log("Spawned Bullet");
                    //spawn = spawnPosSet.pos;
                    rand = UnityEngine.Random.Range(13,15);
                    shellInstance = Instantiate(bullet[2], fireTransform[rand].transform.position, fireTransform[rand].transform.rotation) as Rigidbody;
                    lookPos = player.transform.position - shellInstance.transform.position;
                    shellInstance.transform.rotation = Quaternion.Slerp(shellInstance.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * 10000);
                    shellInstance.velocity = shellInstance.transform.forward * 10;

                    //canSpawn = false;
                    break;
            }
            savedTime[1] = GameManager.GlobalTimer;
        }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Bullet")
        {
            CurrentHealth -= Mathf.Clamp(1, 0, StartHealth);
            BossBarFull.fillAmount = CurrentHealth / StartHealth;
        }
        if(CurrentHealth <= 0 && Time.timeScale != 0)
        {
            Debug.Log("Gate 2");
            // SCENE CHANGE HERE
            switch (bossType)
            {
                case BossType.Bomb:
                    gameManager.LoadNextScene("MissileBossScene");
                    break;
                case BossType.Missle:
                    gameManager.LoadNextScene("BulletBossScene");
                    break;
                case BossType.Bullet:
                    Debug.Log("Boss Dead");
                    break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player" && Time.timeScale != 0)
        {
            Debug.Log("Gate 3");
            savedTime[0] = GameManager.GlobalTimer;
            particle[0].Play();
            target = other.attachedRigidbody;
            Launching = true;
        }
    }
    private void LaunchTarget(Collider Target)
    {

    }
    public void StartFight()
   {
        //for(int i = 0; i <= bossTerrains.Length - 1; i++)
        //{
        //    bossTerrains[i].SetActive(false);
        //}
        dead = false;
        gameObject.SetActive(true);
        CurrentHealth = StartHealth;
        BossBarFull.fillAmount = CurrentHealth / StartHealth;
        //bossTerrains[bTasInt].SetActive(true);
        //gameObject.transform.position = BossSpawns[bTasInt].position;
        player.transform.position = PlayerSpawn.position;
        //LoadBossModel(bTasInt);
        switch (bossType)
        {
            case BossType.Bomb:
                ammo = Ammo.bomb;
                break;
            case BossType.Missle:
                ammo = Ammo.missile;
                break;
            case BossType.Bullet:
                ammo = Ammo.bullet;
                break;
        }
    }
    /*public void LoadBossModel(int i)
    {
        foreach(GameObject model in BossModel)
        {
            model.SetActive(false);
        }
        BossModel[i].SetActive(true);
    }*/
}
