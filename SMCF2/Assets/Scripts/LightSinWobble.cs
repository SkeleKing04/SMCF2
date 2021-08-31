using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSinWobble : MonoBehaviour
{
    public float speed;
    public float size;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(transform.right * (Mathf.Sin(Time.time * speed) * size + 90));
        gameObject.transform.rotation = Quaternion.Euler(transform.up * (Mathf.Sin(Time.time * speed) * size));

    }
}
