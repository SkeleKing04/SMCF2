using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float savedTime;
    public GameObject self;
    private void Awake()
    {
        savedTime = GameManager.GlobalTimer;
    }
    void Update()
    {
        switch (GameManager.GlobalTimer >= savedTime + 3)
        {
            case true:
                Destroy(self);
                break;
        }    
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(self);
    }
}
