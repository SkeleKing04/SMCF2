using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfterTime : MonoBehaviour
{
    private float savedTime;
    public float dieAfter;
    private void Awake()
    {
        savedTime = GameManager.GlobalTimer;
    }

    // Update is called once per frame
    void Update()
    {
        // die when time's up
        if(GameManager.GlobalTimer >= savedTime + dieAfter){
            Destroy(gameObject);
        }
    }
}
