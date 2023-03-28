using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Grid : MonoBehaviour
{
    int _tileSize = 100;
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
        x = Math.Floor(x / _tileSize) * _tileSize + 50;
        y = Math.Floor(y / _tileSize) * _tileSize + 50;
        Debug.Log(x + ", " + y);

        Vector3 pos = new Vector3((float)x, (float)y, 0);
        Quaternion rot = new Quaternion();
        var gameobjectref = Instantiate(SelectedItem.SelectedPrefab, pos, rot);
        Debug.Log(gameobjectref.GetComponent<Item>().prefabID.ToString());
        //levelStorage.Instance.AddItem(gameobjectref);

        await SaveButtonClick.ExampleAsync(SelectedItem.SelectedPrefab, pos);
    }
}
