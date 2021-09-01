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
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Awake()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        Fire();
    }
    private void Fire()
    {
        //lookPos = player.transform.position - rigidbody.transform.position;
        //rigidbody.AddForce(transform.forward * player.transform.position.z, ForceMode.Impulse);
        //rigidbody.AddForce(transform.right * player.transform.position.x, ForceMode.Impulse);
        //rigidbody.AddForce(transform.up * player.transform.position.y, ForceMode.Impulse);
    }
}
