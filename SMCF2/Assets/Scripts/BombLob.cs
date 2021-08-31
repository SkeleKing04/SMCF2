using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLob : MonoBehaviour
{
    public Rigidbody rigidbody;
    private GameObject player;
    private Vector3 lookPos;
    public float explosionForce;
    public float explosionRadius;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        Fire();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Fire()
    {
        lookPos = player.transform.position - rigidbody.transform.position;
        rigidbody.AddForce(transform.forward * (player.transform.position.z / 2), ForceMode.Impulse);
        rigidbody.AddForce(transform.right * (player.transform.position.x / 2), ForceMode.Impulse);
        //rigidbody.AddForce(transform.up * -player.transform.position.y, ForceMode.Impulse);
    }
}
