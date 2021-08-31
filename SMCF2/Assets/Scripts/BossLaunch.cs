using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaunch : MonoBehaviour
{
    public float LaunchForce;
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            other.attachedRigidbody.AddForce(other.transform.up * (LaunchForce / 2), ForceMode.Impulse);
            other.attachedRigidbody.AddForce(other.transform.forward * -LaunchForce, ForceMode.Impulse);

        }
    }
}
