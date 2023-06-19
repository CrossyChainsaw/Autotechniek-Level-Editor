using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadTasks : MonoBehaviour
{
    const string TAG_TASK_NAME = "TaskName";
    const string TAG_TASK_DESCRIPTION = "TaskDescription";
    const string TAG_TASK_TIPS = "TaskTips";

    public Button LoadButton;
    public List<CarTask> TaskList { get; private set; }

    TextMeshProUGUI TaskName;
    TextMeshProUGUI TaskDescription;
    TextMeshProUGUI TaskTips;

    void Start()
    {
        GameModeManager.SetGamemode(Gamemodes.Play);
        TaskList = Data.CarTaskData.LoadCarTasksFromTextFile(); // load cartasks from textfile
        //TaskList = new CarTaskCollection().AllTasks; // HARDCODE: Assign all tasks to player, only use for testing (this is used in the WEBGL build on itch.io)
        LoadButton.GetComponent<Button>().onClick.AddListener(ButtonClick);
        FindTextBoxes();

        void FindTextBoxes()
        {
            TaskName = GameObject.FindGameObjectWithTag(TAG_TASK_NAME).GetComponent<TextMeshProUGUI>();
            TaskDescription = GameObject.FindGameObjectWithTag(TAG_TASK_DESCRIPTION).GetComponent<TextMeshProUGUI>();
            TaskTips = GameObject.FindGameObjectWithTag(TAG_TASK_TIPS).GetComponent<TextMeshProUGUI>();
        }
    }
    void ButtonClick()
    {
        LoadInFirstTask();
    }
    void LoadInFirstTask()
    {
        TaskName.text = TaskList[0].Name;
        TaskDescription.text = TaskList[0].Description;
        TaskTips.text = "";
    }
    public void LoadInTask(CarTask c)
    {
        TaskName.text = c.Name;
        TaskDescription.text = c.Description;
        TaskTips.text = "";
    }
    public void ClearTextBoxes()
    {
        TaskName.text = "";
        TaskDescription.text = "";
        TaskTips.text = "";
    }
    public void Finish()
    {
        TaskName.text = "Je hebt alle taked voltooid";
        TaskDescription.text = "";
        TaskTips.text = "";
    }
}
