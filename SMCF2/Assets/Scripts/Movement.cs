using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private BossAI bossAI;
    private Claws claws;
    public float MoveSpeed;
    public float dashForce;
    public float jumpForce;
    private bool Grounded;
    public bool Dashing;
    private Rigidbody rigidbody;
    private Terrain terrain;
    public GameObject boss;
    public GameObject centerOfCrab;
    private float rotateSpeed = 1000;
    private Vector3 lookPos;
    private float savedTime;
    public ParticleSystem[] dashParticles;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        claws = FindObjectOfType<Claws>();
        rigidbody = GetComponent<Rigidbody>();
        terrain = FindObjectOfType<Terrain>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale != 0)
        {
            //Debug.Log(bossAI);
            switch (bossAI.dead)
            {
                case false:
                    lookPos = boss.transform.position - rigidbody.transform.position;
                    lookPos.y = 0;
                    rigidbody.transform.rotation = Quaternion.Slerp(rigidbody.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotateSpeed);
                    lookPos = boss.transform.position - rigidbody.transform.position;
                    centerOfCrab.transform.rotation = Quaternion.Slerp(rigidbody.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotateSpeed);
                    break;
            }
            if (!Dashing)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        rigidbody.AddForce(transform.forward * dashForce, ForceMode.Impulse);
                    }
                    else
                    {
                        rigidbody.AddForce(transform.forward * -dashForce, ForceMode.Impulse);
                    }
                    savedTime = GameManager.GlobalTimer;
                    Dashing = true;
                
                }
            }
            if (Grounded && Input.GetKeyDown(KeyCode.Space))
            {
                rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            }
            if(Dashing && GameManager.GlobalTimer - savedTime >= 0.4)
            {
                Dashing = false;
            }
        }
    }
    private void FixedUpdate()
    {
        if(Time.timeScale != 0)
        {
            rigidbody.position += (transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * MoveSpeed);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Terrain")
        {
            Grounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.transform.tag == "Terrain")
        {
            Grounded = false;
        }
    }
}
