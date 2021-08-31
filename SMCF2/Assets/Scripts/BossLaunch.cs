using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaunch : MonoBehaviour
{
    public float savedTime;
    public float LaunchForce;
    public ParticleSystem[] particle;
    private bool Launching;
    private Rigidbody target;
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            savedTime = GameManager.GlobalTimer;
            particle[0].Play();
            target = other.attachedRigidbody;
            Launching = true;
        }
    }
    void Update()
    {
        if(GameManager.GlobalTimer - savedTime >= 0.5 && Launching)
        {
            target.AddForce(target.transform.up * (LaunchForce / 2), ForceMode.Impulse);
            target.AddForce(target.transform.forward * -LaunchForce, ForceMode.Impulse);
            particle[1].Play();
            Launching = false;
        }
    }
}
