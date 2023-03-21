using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    public void OnMouseDown()
    {
        SelectedItem.Eraser = true;
        Debug.Log("Eraser True");
    }
}
