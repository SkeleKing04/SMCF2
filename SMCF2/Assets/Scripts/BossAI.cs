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
    // The different projectiles as prefabs
    public Rigidbody bullet;
    // How frequently the projectiles are spawned
    public float rpm;
    // If projectiles can be spawned
    private bool canSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        // Finds the player in the scene
        player = GameObject.FindGameObjectWithTag("Player");
        // Finds the scripts
        gameManager = FindObjectOfType<GameManager>();
        gameObject.SetActive(true);
        CurrentHealth = StartHealth;
        BossBarFull.fillAmount = CurrentHealth / StartHealth;
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
            if(GameManager.GlobalTimer - savedTime[0] >= 0.5 && Launching)
            {
                target.AddForce(target.transform.up * (LaunchForce / 2), ForceMode.Impulse);
                target.AddForce(target.transform.forward * -LaunchForce, ForceMode.Impulse);
                particle[1].Play();
                Launching = false;
            }
            if(canSpawn && GameManager.GlobalTimer - savedTime[1] >= rpm)
            {
                rand = UnityEngine.Random.Range(0,fireTransform.Length);
                Rigidbody shellInstance = Instantiate(bullet, fireTransform[rand].transform.position, fireTransform[rand].transform.rotation) as Rigidbody;
                shellInstance.velocity = 1 * shellInstance.transform.forward;
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
                    gameObject.SetActive(false);
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
}
