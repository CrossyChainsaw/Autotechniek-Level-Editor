using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

// This class is pretty chaotic, sorry

public enum Items
{
    None = -1,
    Player = 0,
    BlueBlock = 1,
    GreenBlock = 2,
    PinkBlock = 3,
    Drill = 4,
    Car = 5,
    Wheel = 6,
    Bolt = 7,
    BoltHole = 8,
    CrossSocketWrench = 9,
    TorqueWrench = 10,
}

public class Item : MonoBehaviour
{
    public int prefabID;
    //public GameObject prefab;
    public bool collectable;
    public Items itemType;
    UIItemCollection _uiItemCollection;
    CarTask1 _carTask1;

    private void Start()
    {
        _uiItemCollection = GameObject.FindGameObjectWithTag("UIItemCollection").GetComponent<UIItemCollection>();
        _carTask1 = GameObject.FindGameObjectWithTag("Minigame1").GetComponent<CarTask1>();

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && collectable == true && GameModeManager.Gamemode == Gamemodes.Play)
        {
            Destroy(this.gameObject);
        }
    }
    public async void OnMouseDown()
    {
        if (GameModeManager.Gamemode == Gamemodes.CarTask)
        {
            _carTask1.Minigame_Item(prefabID, this.gameObject);
        }

        // Eraser?
        if (_uiItemCollection.EraserSelected == true && GameModeManager.Gamemode == Gamemodes.Level_Editor)
        {
            GameObject destroyedObject = gameObject;
            Destroy(gameObject);
            Debug.Log("Destroyed ig");
            Item destroyedItem = destroyedObject.GetComponent<Item>();
            List<(int id, int x, int y)> data = Data.ReadDataFromTextFile();

            (int id, int x, int y) removeThisEntry = (-1, -1, -1);
            int i = 0;
            foreach ((int id, int x, int y) entry in data)
            {
                if (entry.id == destroyedItem.prefabID && entry.x == destroyedObject.transform.position.x && entry.y == destroyedObject.transform.position.y)
                {
                    removeThisEntry = entry;
                    break;
                }
                i++;
            }
            data.Remove(removeThisEntry);
            await Data.Overwrite(data);
        }
        else
        {
            Debug.Log("Eraser: " + _uiItemCollection.EraserSelected);
            Debug.Log("Gamemode: " + GameModeManager.Gamemode);
        }
        Debug.Log("Clicked on " + itemType);
    }
}

