using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameModeManager.SetGamemode.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
