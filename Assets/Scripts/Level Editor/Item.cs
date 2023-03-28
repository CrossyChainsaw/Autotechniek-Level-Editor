using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

// This class is pretty chaotic, sorry

enum Items
{
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
}

public class Item : MonoBehaviour
{
    //public Items item;
    public int prefabID;
    public GameObject prefab;
    public bool collectable;

    public Item(int prefabID)
    {
        this.prefabID = prefabID;
    }

    // onlangs toegevoegd, letop werkt nog niet
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && collectable == true)
        {
            Destroy(this.gameObject);
        }
    }

    public async void OnMouseDown()
    {
        if (prefab == null)
        {
            // Minigame 1
            if (prefabID == (int)Items.Bolt && Minigame1.currentTask == Minigame1Tasks.Task1)
            {
                this.gameObject.SetActive(false);
                Minigame1.RemoveBolt();
            }
            else if (prefabID == (int)Items.Wheel && Minigame1.currentTask == Minigame1Tasks.Task2)
            {
                Minigame1.someRef = gameObject;
                this.gameObject.SetActive(false);
                Minigame1.RemoveWheel();
            }
            else if (prefabID == (int)Items.Wheel && Minigame1.currentTask == Minigame1Tasks.Task3)
            {
                GameObject gameObject = Minigame1.someRef;
                gameObject.SetActive(true);
                this.gameObject.SetActive(false);
                Minigame1.PlaceWheel();
            }
            else if (prefabID == (int)Items.BoltHole && Minigame1.currentTask == Minigame1Tasks.Task4)
            {
                this.gameObject.SetActive(false);
                Minigame1.PlaceBolt();
            }

            // Editor?
            if (SelectedItem.Eraser == true && GameModeManager.Gamemode == Gamemodes.Level_Editor)
            {
                GameObject destroyedObject = gameObject;
                Destroy(gameObject);
                Debug.Log("Destroyed ig");
                Item destroyedItem = destroyedObject.GetComponent<Item>();
                List<(int id, int x, int y)> data = ReadDataFromTextFile();

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
        }
        else
        {
            SelectedItem.SelectedPrefab = prefab;
            SelectedItem.Eraser = false;
            Debug.Log("Selected " + prefab.ToString());
        }
        Debug.Log("Clicked on " + prefab);
    }

    List<(int id, int x, int y)> ReadDataFromTextFile()
    {
        List<(int id, int x, int y)> data = new List<(int id, int x, int y)>();
        List<int> dataParts = new List<int>();
        int count = 0;

        foreach (string line in System.IO.File.ReadLines("Editor.txt"))
        {
            Debug.Log(line);
            dataParts.Add(Convert.ToInt32(line));
            count++;
            if (count == 3)
            {
                data.Add((dataParts[0], dataParts[1], dataParts[2]));
                dataParts.Clear();
                count = 0;
                // test this shit
            }
        }
        Debug.Log(data[0]);
        Debug.Log(data[1]);
        return data;
    }
}

