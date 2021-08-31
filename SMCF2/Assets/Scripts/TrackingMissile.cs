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
        rigidbody.position += (transform.forward * MoveSpeed * Time.deltaTime);
        if(GameManager.GlobalTimer > savedTime + 3)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag != "Boss" || collision.transform.tag != "Enemy Missile")
        {
            Destroy(gameObject);
            
        }
    }
}
