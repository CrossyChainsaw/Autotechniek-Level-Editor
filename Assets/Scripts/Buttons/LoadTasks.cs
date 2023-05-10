using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadTasks : MonoBehaviour
{
    public CarTaskCollection CarTaskCollection { get; private set; }

    void Start()
    {
        GameModeManager.SetGamemode(Gamemodes.Play);
        CarTaskCollection = new CarTaskCollection();
        LoadInFirstTask();
        Debug.Log(GameModeManager.Gamemode);
    }

    void LoadInFirstTask()
    {
        TextMeshProUGUI TaskName = GameObject.FindGameObjectWithTag("TaskName").GetComponent<TextMeshProUGUI>();
        TaskName.text = CarTaskCollection.AllTasks[0].Name;
        TextMeshProUGUI TaskDescription = GameObject.FindGameObjectWithTag("TaskDescription").GetComponent<TextMeshProUGUI>();
        TaskDescription.text = CarTaskCollection.AllTasks[0].Description;
        TextMeshProUGUI TaskTips = GameObject.FindGameObjectWithTag("TaskTips").GetComponent<TextMeshProUGUI>();
        TaskTips.text = "placeholder";
    }
}
