using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

// This class is pretty chaotic, sorry

// I've put the Dutch names of tools next to it since you will encounter them in strings in the application
public enum Items
{
    None = -1,
    Player = 0,
    BlueBlock = 1,
    GreenBlock = 2,
    PinkBlock = 3,
    Drill = 4, // Boor
    Car = 5, // Auto
    Wheel = 6, // Wiel
    Bolt = 7, // Schroef
    BoltHole = 8, // Gat zonder schroef
    CrossSocketWrench = 9, // Kruissleutel
    TorqueWrench = 10, // Moment Sleutel
    EngineHood = 11, // Motor Kap
    CarBattery = 12, // Accu
    Wrench = 13, // Steek Sleutel
}

public class Item : MonoBehaviour
{
    public int PrefabID { get { return (int)ItemType; } }
    public bool Collectable; // WARNING - since this property is assigned inside unity it will take a very longtime to re-assign everything, so try not to rename this property
    public Items ItemType; // WARNING - since this property is assigned inside unity it will take a very longtime to re-assign everything, so try not to rename this property
    UIItemCollection _uiItemCollection;
    CarTask1 _carTask1;

    private void Start()
    {
        _uiItemCollection = GameObject.FindGameObjectWithTag("UIItemCollection").GetComponent<UIItemCollection>();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && Collectable == true && GameModeManager.Gamemode == Gamemodes.Play)
        {
            Destroy(this.gameObject);
        }
    }
    public async void OnMouseDown()
    {
        if (GameModeManager.Gamemode == Gamemodes.CarTask)
        {
            _carTask1 = (CarTask1)GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().TaskList[0];
            _carTask1.Minigame_Item(PrefabID, this.gameObject);
        }

        // Eraser?
        if (_uiItemCollection.EraserSelected == true && GameModeManager.Gamemode == Gamemodes.Level_Editor)
        {
            GameObject destroyedObject = gameObject;
            Destroy(gameObject);
            Debug.Log("Destroyed ig");
            Item destroyedItem = destroyedObject.GetComponent<Item>();
            List<(int id, int x, int y)> data = Data.GridData.ReadDataFromTextFile();

            (int id, int x, int y) removeThisEntry = (-1, -1, -1);
            int i = 0;
            foreach ((int id, int x, int y) entry in data)
            {
                if (entry.id == destroyedItem.PrefabID && entry.x == destroyedObject.transform.position.x && entry.y == destroyedObject.transform.position.y)
                {
                    removeThisEntry = entry;
                    break;
                }
                i++;
            }
            data.Remove(removeThisEntry);
            await Data.GridData.Overwrite(data);
        }
        else
        {
            Debug.Log("Eraser: " + _uiItemCollection.EraserSelected);
            Debug.Log("Gamemode: " + GameModeManager.Gamemode);
        }
        Debug.Log("Clicked on " + ItemType);
    }
}

