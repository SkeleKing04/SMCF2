using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalSpawnTrail : MonoBehaviour
{
    public ParticleSystem Particle;
    private ParticleSystem NewParticle;
    private float savedTime;
    
    private void Awake()
    {
        savedTime = GameManager.GlobalTimer;
    }
    // Update is called once per frame
    void Update()
    {
        if(GameManager.GlobalTimer - savedTime > 0.1f)
        {
            Debug.Log("particle spawning");
            NewParticle = Instantiate(Particle, gameObject.transform.position, gameObject.transform.rotation);
            NewParticle.Play();
            savedTime = GameManager.GlobalTimer;
        }
        if(GameManager.GlobalTimer - savedTime > 1f)
        {
            Destroy(NewParticle);
            savedTime = GameManager.GlobalTimer;
        }
    }
}
