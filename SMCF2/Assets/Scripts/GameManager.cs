using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float GlobalTimer;
    // Start is called before the first frame update
    void Start()
    {
        GlobalTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GlobalTimer += Time.deltaTime;
    }
    private void FixedUpdate()
    {

    }
}
