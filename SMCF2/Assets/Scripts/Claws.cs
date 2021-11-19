using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Claws : MonoBehaviour
{
    // scripts from other objects
    private Movement movementScript;
    private GameManager gameManager;
    // crab claws
    public Transform[] claws;
    // where bullets spawn at
    public Transform[] fireTransforms;
    // projectiles to spawn
    public Rigidbody bullet;
    // force to fire bullets with
    public float bulletForce;
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        movementScript = FindObjectOfType<Movement>();
        gameManager = FindObjectOfType<GameManager>();
        boss = GameObject.FindWithTag("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        // if the game isn't paused
        if(Time.timeScale != 0)
        {
            // and the player isn't dashing
            if (!movementScript.Dashing)
            {
                // if the left mouse is clicked
                if (Input.GetButtonDown("Fire1"))
                {
                    // spawn the bullet a the left fire transform
                    Vector3 lookPos = boss.transform.position - fireTransforms[0].transform.position;
                    Rigidbody shellInstance = Instantiate(bullet, fireTransforms[0].transform.position, Quaternion.Slerp(fireTransforms[0].transform.rotation, Quaternion.LookRotation(lookPos), 1000)) as Rigidbody;
                    shellInstance.velocity = bulletForce * fireTransforms[0].up;
                }
                if (Input.GetButtonDown("Fire2"))
                {
                    // spawn the bullet a the right fire transform
                    Vector3 lookPos = boss.transform.position - fireTransforms[1].transform.position;
                    Rigidbody shellInstance = Instantiate(bullet, fireTransforms[1].transform.position, Quaternion.Slerp(fireTransforms[1].transform.rotation, Quaternion.LookRotation(lookPos), 1000)) as Rigidbody;
                    shellInstance.velocity = bulletForce * fireTransforms[1].up;
                }
            }
        }
    }
}
