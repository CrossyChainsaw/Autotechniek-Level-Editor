using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadTasks : MonoBehaviour
{
    public Button LoadButton;
    public List<CarTask> TaskList { get; private set; }
    string _taskNameTag = "TaskName";
    string _taskDescriptionTag = "TaskDescription";
    string _taskTipsTag = "TaskTips";

    void Start()
    {
        GameModeManager.SetGamemode(Gamemodes.Play);
        TaskList = Data.CarTaskData.LoadCarTasksFromTextFile(); // load cartasks from textfile
        //TaskList = new CarTaskCollection().AllTasks; // HARDCODE: Assign all tasks to player, only use for testing (this is used in the itch version)
        LoadButton.GetComponent<Button>().onClick.AddListener(ButtonClick);
    }
    void ButtonClick()
    {
        LoadInFirstTask();
    }
    void LoadInFirstTask()
    {
        TextMeshProUGUI TaskName = GameObject.FindGameObjectWithTag(_taskNameTag).GetComponent<TextMeshProUGUI>();
        TaskName.text = TaskList[0].Name;
        TextMeshProUGUI TaskDescription = GameObject.FindGameObjectWithTag(_taskDescriptionTag).GetComponent<TextMeshProUGUI>();
        TaskDescription.text = TaskList[0].Description;
        TextMeshProUGUI TaskTips = GameObject.FindGameObjectWithTag(_taskTipsTag).GetComponent<TextMeshProUGUI>();
        TaskTips.text = "";
    } // tip: watchout for removing tags, i check on tagnames quite a lot
    public void LoadInTask(CarTask c)
    {
        TextMeshProUGUI TaskName = GameObject.FindGameObjectWithTag(_taskNameTag).GetComponent<TextMeshProUGUI>();
        TaskName.text = c.Name;
        TextMeshProUGUI TaskDescription = GameObject.FindGameObjectWithTag(_taskDescriptionTag).GetComponent<TextMeshProUGUI>();
        TaskDescription.text = c.Description;
        TextMeshProUGUI TaskTips = GameObject.FindGameObjectWithTag(_taskTipsTag).GetComponent<TextMeshProUGUI>();
        TaskTips.text = "";
    }
}
