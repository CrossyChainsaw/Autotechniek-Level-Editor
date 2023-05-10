using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayMode : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject Minigame1Camera;

    void Start()
    {
        GameModeManager.SetGamemode(Gamemodes.Play);
    }
}
