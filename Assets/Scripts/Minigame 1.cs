using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Minigame1Tasks
{
    Task1,
    Task2,
    Task3,
    Task4,
    Task5
}

//phase1 remove bolts
//p2 take off wheel
//p3new wheel in
//p4 put back in bolts

public static class Minigame1
{
    static int nBolts = 5;
    static int nWheels = 1;
    public static Minigame1Tasks currentTask = Minigame1Tasks.Task1;
    public static GameObject someRef;

    public static void RemoveBolt()
    {
        nBolts--;
        if (nBolts == 0)
        {
            NextPhase();
        }
    }
    public static void RemoveWheel()
    {
        nWheels--;
        NextPhase();
    }
    public static void PlaceWheel()
    {
        nWheels++;
        NextPhase();
    }
    public static void PlaceBolt()
    {
        nBolts++;
        if (nBolts == 5)
        {
            SwitchCamBackToGame();
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.EnableControls();
        }
    }

    public static void NextPhase()
    {
        int i = (int)currentTask;
        i++;
        currentTask = (Minigame1Tasks)i;
        Debug.Log(currentTask);
    }
    public static void SwitchCamBackToGame()
    {
        GameObject MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        MainCamera.transform.position = new Vector3(0, 0, -10);
    }
}
