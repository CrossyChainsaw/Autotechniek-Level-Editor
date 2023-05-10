using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTaskCollection
{
    // Tasks is linked with the Player class
    public List<CarTask> Tasks { get; private set; }
    public CarTaskCollection()
    {
        Tasks = new List<CarTask>();

        // These are in Dutch because they will be shown inside the game (sorry)
        Tasks.Add(new CarTask("Verwissel een Wiel", Items.Wheel, "Pak de Kruissleutel en de Momentsleutel en interact met het wiel", "Je moet eerst de benodigde items oppakken", Items.CrossSocketWrench, Items.TorqueWrench, Items.Wheel));
    }

    /*
    so what should happen is
    Editor
    - you go in the editor
    - whilst creating the level you can see which tasks can be done. They unlock or lock based off the tools in the scene
    - after making the level you should be able to order them somehow?
    - task should prevent someone from doing other tasks

    Play
    - load in the tasks from the data file to the player
    - make the player only do the first task, also load in the first task
    */ 
}
