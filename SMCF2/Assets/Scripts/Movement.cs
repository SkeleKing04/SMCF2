using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameManager gameManager;
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
        Debug.Log("Vertical = " + Input.GetAxis("Vertical"));
        Debug.Log("Horizontal = " + Input.GetAxis("Horizontal"));

        lookPos = boss.transform.position- rigidbody.transform.position;
        Debug.Log("lookPos is " + lookPos);
        //centerOfCrab.transform.rotation = Quaternion.Slerp(rigidbody.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotateSpeed);
        lookPos.y = 0;
        rigidbody.transform.rotation = Quaternion.Slerp(rigidbody.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotateSpeed);


        if (!Dashing)
        {
            rigidbody.position += (transform.right * Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime);
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                if (Input.GetKeyUp(KeyCode.W))
                {
                    rigidbody.AddForce(transform.forward * dashForce, ForceMode.Impulse);
                }
                if (Input.GetKeyUp(KeyCode.S))
                {
                    rigidbody.AddForce(transform.forward * -dashForce, ForceMode.Impulse);

                }
                rigidbody.AddForce(transform.right * Input.GetAxis("Horizontal") * dashForce, ForceMode.Impulse);
                savedTime = gameManager.GlobalTimer;
                Dashing = true;
            }
        }
        if (Grounded)
        {
            rigidbody.AddForce(transform.up * Input.GetAxis("Jump") * jumpForce, ForceMode.Impulse);
        }
        if(Dashing && gameManager.GlobalTimer - savedTime >= 0.75)
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
