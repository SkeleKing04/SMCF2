using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinDraw : MonoBehaviour
{
    //Sets the position
    public Vector3 pos;
    //Speed the object moves at
    public float Speed;
    //Size that objects moves
    public float Size;
    //Multiplier along the X
    public float xMulti;
        //Multiplier along the y
    public float yMulti;
        //Multiplier along the z
    public float zMulti;
        //Offsets from 0,0,0 
    public float xOffset;
    public float yOffset;
    public float zOffset;
    //Changes wether or not it calculated using Sin Cos or Tan
    // Sin = 0
    // Cos = 1
    // Tan = 2
    public int[] SinCosTan = new int[3];
    void Update()
    {
        switch (SinCosTan[0])
        {
            case 0:
                pos.x = xOffset + Mathf.Sin(Time.time * Speed * xMulti) * Size;
                break;
            case 1:
                pos.x = xOffset + Mathf.Cos(Time.time * Speed * xMulti) * Size;
                break;
            case 2:
                pos.x = xOffset + Mathf.Tan(Time.time * Speed * xMulti) * Size;
                break;
        }
        switch (SinCosTan[1])
        {
            case 0:
                pos.y = yOffset + Mathf.Sin(Time.time * Speed * yMulti) * Size;
                break;
            case 1:
                pos.y = yOffset + Mathf.Cos(Time.time * Speed * yMulti) * Size;
                break;
            case 2:
                pos.y = yOffset + Mathf.Tan(Time.time * Speed * yMulti) * Size;
                break;
        }
        switch (SinCosTan[2])
        {
            case 0:
                pos.z = zOffset + Mathf.Sin(Time.time * Speed * zMulti) * Size;
                break;
            case 1:
                pos.z = zOffset + Mathf.Cos(Time.time * Speed * zMulti) * Size;
                break;
            case 2:
                pos.z = zOffset + Mathf.Tan(Time.time * Speed * zMulti) * Size;
                break;
        }
    }
}
