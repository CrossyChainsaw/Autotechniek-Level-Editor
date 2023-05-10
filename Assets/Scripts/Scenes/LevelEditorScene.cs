using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorScene : MonoBehaviour 
{
    void Start()
    {
        GameModeManager.SetGamemode(Gamemodes.Level_Editor);
    }
}
