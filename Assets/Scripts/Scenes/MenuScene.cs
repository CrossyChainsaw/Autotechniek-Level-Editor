using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScene : MonoBehaviour
{
    void Start()
    {
        GameModeManager.SetGamemode(Gamemodes.Menu);
    }
}
