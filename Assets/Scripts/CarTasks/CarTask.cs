using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTask
{
    public string Name { get; private set; }
    /// <summary>The Item the player has to collide with in the scene to start the corresponding CarTask</summary>
    public Items StartItem { get; private set; }
    public string Description { get; private set; }
    /// <summary>If you try to interact with the correct part of the car but you don't have all the necessary tools, it will tell you</summary>
    public string InteractFeedback { get; private set; }
    public Items[] RequiredTools { get; private set; }


    public CarTask(string name, Items startItem, string description, string interactFeedback, params Items[] itemArray)
    {
        Name = name;
        Description = description;
        StartItem = startItem;
        RequiredTools = itemArray;
    }
}
