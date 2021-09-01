using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerTrack : MonoBehaviour
{
    private Vector3 lookPos;
    private GameObject player;
    private Vector3 initialPos;
    public float rotateSpeed;
    public GameObject fireTransform;
    //private GameObject rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        //rigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        initialPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        lookPos = player.gameObject.transform.position - gameObject.transform.position;
        lookPos.y = 0;
        gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotateSpeed);
        lookPos = player.gameObject.transform.position - gameObject.transform.position;
        //Quaternion test = gameObject.transform.rotation;
        //test.x += 90;
        fireTransform.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * rotateSpeed);
    }
}
