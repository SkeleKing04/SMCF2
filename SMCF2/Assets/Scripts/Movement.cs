using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameManager gameManager;
    private BossHeath bossHeath;
    public Claws claws;
    public float MoveSpeed;
    public float dashForce;
    public float jumpForce;
    public bool Grounded;
    public bool Dashing;
    private Rigidbody rigidbody;
    private Terrain terrain;
    public GameObject boss;
    public GameObject centerOfCrab;
    public float rotateSpeed;
    private Vector3 lookPos;
    public float savedTime;
    public ParticleSystem[] dashParticles;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        bossHeath = FindObjectOfType<BossHeath>();
        claws = FindObjectOfType<Claws>();
        rigidbody = GetComponent<Rigidbody>();
        terrain = FindObjectOfType<Terrain>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (bossHeath.dead)
        {
            case false:
                //Debug.Log("Vertical = " + Input.GetAxis("Vertical"));
                //Debug.Log("Horizontal = " + Input.GetAxis("Horizontal"));
                lookPos = boss.transform.position- rigidbody.transform.position;
                //Debug.Log("lookPos is " + lookPos);
                //centerOfCrab.transform.rotation = Quaternion.Slerp(rigidbody.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotateSpeed);
                lookPos.y = 0;
                rigidbody.transform.rotation = Quaternion.Slerp(rigidbody.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotateSpeed);
                lookPos = boss.transform.position - rigidbody.transform.position;
                centerOfCrab.transform.rotation = Quaternion.Slerp(rigidbody.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotateSpeed);
                break;
        }

            rigidbody.position += (transform.right * Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime);

        if (!Dashing)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    rigidbody.AddForce(transform.forward * dashForce, ForceMode.Impulse);
                    ParticleSystem dashParticle = Instantiate(dashParticles[0], transform.position, gameObject.transform.rotation) as ParticleSystem;
                }
                else
                {
                    rigidbody.AddForce(transform.forward * -dashForce, ForceMode.Impulse);
                    ParticleSystem dashParticle = Instantiate(dashParticles[1], transform.position, gameObject.transform.rotation) as ParticleSystem;
                }
                rigidbody.AddForce(transform.right * Input.GetAxis("Horizontal") * dashForce, ForceMode.Impulse);
                savedTime = GameManager.GlobalTimer;
                Dashing = true;
                
            }
        }
        if (Grounded)
        {
            rigidbody.AddForce(transform.up * Mathf.Round(Input.GetAxis("Jump")) * jumpForce, ForceMode.Impulse);
        }
        if(Dashing && GameManager.GlobalTimer - savedTime >= 0.4)
        {
            Dashing = false;
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
