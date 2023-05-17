using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarTask : MonoBehaviour // need monobehaviour solely for Destroy()
{
    public int ID { get; internal set; }
    public string Name { get; internal set; }
    /// <summary>The Item the player has to collide with in the scene to start the corresponding CarTask</summary>
    public Items StartItem { get; internal set; }
    public string Description { get; internal set; }
    /// <summary>The Required Tools to start the corresponding CarTask</summary>
    public Items[] RequiredTools { get; internal set; }

    public CarTask(int id, string name, Items startItem, string description, params Items[] itemArray)
    {
        ID = id;
        Name = name;
        Description = description;
        StartItem = startItem;
        RequiredTools = itemArray;
    }
    /// <summary>Only use this when you need to assign</summary>
    public abstract void Activate();
    public abstract void Deactivate();
}
