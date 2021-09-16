using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Claws : MonoBehaviour
{
    private Movement movementScript;
    private GameManager gameManager;
    public Transform[] claws;
    public Transform[] fireTransforms;
    public Rigidbody bullet;
    public float bulletForce;
    // Start is called before the first frame update
    void Start()
    {
        movementScript = FindObjectOfType<Movement>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.gameStateOrder[gameManager.gameStateOrder.Count - 1] == GameManager.GameState.Playing)
        {
//            Debug.Log("Gate 4");
        if (!movementScript.Dashing)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("left firing");
                Rigidbody shellInstance = Instantiate(bullet, fireTransforms[0].transform.position, fireTransforms[0].transform.rotation) as Rigidbody;
                shellInstance.velocity = bulletForce * fireTransforms[0].up;
            }
            if (Input.GetButtonDown("Fire2"))
            {
                Debug.Log("Right Firing");
                Rigidbody shellInstance = Instantiate(bullet, fireTransforms[1].transform.position, fireTransforms[1].transform.rotation) as Rigidbody;
                shellInstance.velocity = bulletForce * fireTransforms[1].up;
            }
        }
        }
    }
    // Set the shell's velocity to the launch force in the fire
    // position's forward direction

}
