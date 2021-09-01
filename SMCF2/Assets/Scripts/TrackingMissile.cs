using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingMissile : MonoBehaviour
{
    public GameObject player;
    private Rigidbody rigidbody;
    public float MoveSpeed;
    public float rotateSpeed;
    private Vector3 lookPos;
    private float savedTime;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Awake()
    {
        savedTime = GameManager.GlobalTimer;
    }

    // Update is called once per frame
    void Update()
    {
        lookPos = player.transform.position- rigidbody.transform.position;
        rigidbody.transform.rotation = Quaternion.Slerp(rigidbody.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotateSpeed);
        rigidbody.transform.position += (transform.forward * MoveSpeed * Time.deltaTime);
        if(GameManager.GlobalTimer > savedTime + 100)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject.name + " has collided with " + collision.gameObject.name);
        if(collision.transform.tag != "Enemy Bullet" && collision.transform.tag != "Boss")
        {
            Destroy(gameObject);
        }
    }
}
