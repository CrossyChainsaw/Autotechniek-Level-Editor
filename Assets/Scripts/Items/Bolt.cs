using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public bool isRemoved = false;

    public void RemoveBolt()
    {
        isRemoved = true;
    } 
}
