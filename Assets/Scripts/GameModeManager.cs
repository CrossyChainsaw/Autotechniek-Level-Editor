using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Gamemodes
{
    Menu,
    Level_Editor,
    Play,
    CarTask // aka minigame
}

// heb dit static gemaakt omdat ik het wil gebruiken in alle scenes en data tijdelijk wil opslaan.
// ik kan ook DontDestroyOnLoad gebruiken, dan blijft he object leven maar static voelt meer natuurlijk om hier te gebruiken
public class GameModeManager 
{
    public static Gamemodes Gamemode { get; private set; }
    public static void SetGamemode(Gamemodes g)
    {
        Gamemode = g;
    }
}
