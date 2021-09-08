using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    private BossHeath bossHealth;
    private BossLaunch bossLaunch;
    private BossPlayerTrack bossPlayerTrack;
    private BossShooting bossShooting;
    public enum BossType
    {
        Bomb,
        Missle,
        Bullet
    };
    public BossType bossType;
    public int bossTypeAsInt;
    public GameObject[] bossTerrains;
    public Transform[] PlayerSpawns;
    public Transform[] BossSpawns;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        bossHealth = FindObjectOfType<BossHeath>();
        bossLaunch = FindObjectOfType<BossLaunch>();
        bossPlayerTrack =FindObjectOfType<BossPlayerTrack>();
        bossShooting = FindObjectOfType<BossShooting>();
        bossTerrains = GameObject.FindGameObjectsWithTag("Terrain");
        player = GameObject.FindGameObjectWithTag("Player");
        for(int i = 0; i <= bossTerrains.Length - 1; i++)
        {
            bossTerrains[i].SetActive(false);
        }
        BossTypeCheck();
    }

    // Update is called once per frame
    void Update()
    {
        if(bossHealth.defeated)
        {
            if(bossTypeAsInt <= 2)
            {
                bossHealth.dead = true;
            }
            bossTypeAsInt += Mathf.Clamp(1, 0, 2);
            bossHealth.NextBoss();
            BossTypeCheck();
        }
    }
    public void BossTypeCheck()
    {
        bossTerrains[bossTypeAsInt].SetActive(true);
        gameObject.transform.position = BossSpawns[bossTypeAsInt].position;
        player.transform.position = PlayerSpawns[bossTypeAsInt].position;
        switch(bossTypeAsInt)
        {
            case 0:
                bossType = BossType.Bomb;
                                bossShooting.ammo = BossShooting.Ammo.bomb;
                bossShooting.shootingArangement = BossShooting.ShootingArangement.Starfire;
                break;
                case 1:
                bossType = BossType.Missle;
                                bossShooting.ammo = BossShooting.Ammo.missile;
                bossShooting.shootingArangement = BossShooting.ShootingArangement.LowWideMoving;
                break;
                case 2:
                bossType = BossType.Bullet;
                                bossShooting.ammo = BossShooting.Ammo.bullet;
                bossShooting.shootingArangement = BossShooting.ShootingArangement.Gattling;
                break;
        }
    }
}
