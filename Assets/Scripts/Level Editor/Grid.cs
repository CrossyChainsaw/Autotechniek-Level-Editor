using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Grid : MonoBehaviour
{
    int _tileSize = 100;
    private UIItemCollection _uiItemCollection;
    private void Start()
    {
        _uiItemCollection = GameObject.FindGameObjectWithTag("UIItemCollection").GetComponent<UIItemCollection>();
    }
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

        // get the center point of the grid tile you clicked in // looks complicated but if you fill in tilesize as 100 it starts to make sense
        x = Math.Floor(x / _tileSize) * _tileSize + (_tileSize / 2);
        y = Math.Floor(y / _tileSize) * _tileSize + (_tileSize / 2);
        Debug.Log(x + ", " + y);

        Vector3 pos = new Vector3((float)x, (float)y, 0);
        Instantiate(_uiItemCollection.SelectedPrefab, pos, new Quaternion());

        // autosave
        await SaveButtonClick.ExampleAsync(_uiItemCollection.SelectedPrefab, pos);
    }
}
