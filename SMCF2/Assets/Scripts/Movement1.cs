using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement1 : MonoBehaviour
{
    public float jumpForce;
    private bool Grounded;
    private Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
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
