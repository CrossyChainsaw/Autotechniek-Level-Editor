using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Grid : MonoBehaviour
{
    int tileSize = 100;
    int gridCorrection = 500;
    
    public void OnMouseDown()
    {
        // get the mouse location in the game
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        PlaceItemOnScreen(mousePosition);
    }
    async void PlaceItemOnScreen(Vector3 mousePosition)
    {
        double x = mousePosition.x;
        double y = mousePosition.y;

        // get the center point of the grid tile you clicked in
        x = Math.Floor(x / tileSize) * tileSize + 50;
        y = Math.Floor(y / tileSize) * tileSize + 50;
        Debug.Log(x + ", " + y);

        Vector3 pos = new Vector3((float)x, (float)y, 0);
        Quaternion rot = new Quaternion();
        var gameobjectref = Instantiate(SelectedItem.selectedPrefab, pos, rot);
        Debug.Log(gameobjectref.GetComponent<Item>().prefabID.ToString());
        //levelStorage.Instance.AddItem(gameobjectref);

        await SaveButtonClick.ExampleAsync(SelectedItem.selectedPrefab, pos);
    }
}
