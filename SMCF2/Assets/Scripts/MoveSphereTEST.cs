using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSphereTEST : MonoBehaviour
{
    public SinDraw sinDraw;
    // Start is called before the first frame update
    void Start()
    {
        sinDraw = GetComponent<SinDraw>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = sinDraw.pos;
    }
}
