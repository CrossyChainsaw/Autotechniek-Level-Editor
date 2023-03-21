using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Gamemodes
{
    Menu,
    Level_Editor,
    Play
}

public class GameModeManager
{
    public static Gamemodes Gamemode { get; private set; }
    static void Set_Gamemode(Gamemodes g)
    {
        Gamemode = g;
    }
    public static class SetGamemode
    {
        public static void Play()
        {
            Set_Gamemode(Gamemodes.Play);
        }
        public static void Menu()
        {
            Set_Gamemode(Gamemodes.Menu);
        }
        public static void Level_Editor()
        {
            Set_Gamemode(Gamemodes.Level_Editor);
        }
    }
}
