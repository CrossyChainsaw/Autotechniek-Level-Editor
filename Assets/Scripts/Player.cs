using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public Transform movePoint;
    public SpriteRenderer spriteRenderer;
    public LayerMask stopMovementLayer;
    public List<CarTask> TaskList { get; private set; }
    Inventory _inventory;

    private void Start()
    {
        _inventory = new Inventory();
        TaskList = Data.CarTaskData.LoadCarTasksFromTextFile();
        //TaskList = GameObject.FindGameObjectWithTag("LoadSaveButton").GetComponent<LoadTasks>().CarTaskCollection.AllTasks; // links CarTaskCollection Constructor with player
    }
    void Update()
    {
        Movement();
    }

    // Collision/Interaction
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (GameModeManager.Gamemode == Gamemodes.Play)
        {
            Item item = col.gameObject.GetComponent<Item>();
            Debug.Log(item.ItemType);

            // Collect any collectable item
            if (item.Collectable == true)
            {
                _inventory.AddItem(item);
            }
            else if (item.ItemType == Items.Wheel)
            {
                if (_inventory.HasItem(TaskList[0].RequiredTools))
                {
                    ((CarTask1)TaskList[0]).Activate();
                    DisableControls();
                }
                else
                {
                    PrintRequiredTools();
                }
            }
            else if (item.ItemType == Items.EngineHood)
            {
                if (_inventory.HasItem(TaskList[0].RequiredTools))
                {
                    ((CarTask2)TaskList[0]).Activate();
                    DisableControls();
                }
                else
                {
                    PrintRequiredTools();
                }
            }
            // foreach task // if you have required items activate // else print req tools
        }
    }
    public void FinishCurrentTask()
    {
        // switch back cam
        GameObject MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        MainCamera.transform.position = new Vector3(0, 0, -10);
        GameObject Inventory = GameObject.FindGameObjectWithTag("Inventory");
        Inventory.transform.position = new Vector3(835, 0, 2);
        Debug.Log(7);
        // remove task
        TaskList.Remove(TaskList[0]);
        Debug.Log(7);
        // load in next task
        GameObject.FindGameObjectWithTag("LoadSaveButton").GetComponent<LoadTasks>().LoadInTask(TaskList[0]);
        Debug.Log(7);
        EnableControls();
    }
    void PrintRequiredTools()
    {
        print("You didn't collect the required tools. Required Tools: ");
        foreach (var item in TaskList[0].RequiredTools)
        {
            if (!_inventory.ItemList.Contains(item))
            {
                Debug.Log(item);
            }
        }
    }

    // Inventory
    /// <summary>Removes a wheel from the player's inventory</summary>
    public void UseItem(Items item)
    {
        _inventory.RemoveItem(item);
    }

    // Controls
    public void DisableControls()
    {
        GameModeManager.SetGamemode(Gamemodes.CarTask);
    }
    public void EnableControls()
    {
        GameModeManager.SetGamemode(Gamemodes.Play);
    }

    // Movement
    /// <summary>Moves the player in a direction using the arrow keys</summary>
    void Movement()
    {
        Debug.Log(GameModeManager.Gamemode);
        // only enable movement inside play mode
        if (GameModeManager.Gamemode == Gamemodes.Play)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                FaceRight();
                // Check if the space you try to move in contains an object with the layer "StopMovement" (Or what ever the variable stopMovementLayer has been assigned to)
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(moveSpeed, 0f, 0f), float.MinValue, stopMovementLayer))
                {
                    MoveForward();
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                FaceLeft();
                if (!Physics2D.OverlapCircle(movePoint.position - new Vector3(moveSpeed, 0f, 0f), float.MinValue, stopMovementLayer))
                {
                    MoveForward();
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                FaceDown();
                if (!Physics2D.OverlapCircle(movePoint.position - new Vector3(0f, moveSpeed, 0f), float.MinValue, stopMovementLayer))
                {
                    MoveForward();
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                FaceUp();
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, moveSpeed, 0f), float.MinValue, stopMovementLayer))
                {
                    MoveForward();
                }
            }
        }
    }
    void MoveUp()
    {
        movePoint.position += new Vector3(0f, moveSpeed, 0f);
    }
    void MoveRight()
    {
        movePoint.position += new Vector3(moveSpeed, 0f, 0f);
    }
    void MoveDown()
    {
        movePoint.position -= new Vector3(0f, moveSpeed, 0f);
    }
    void MoveLeft()
    {
        movePoint.position -= new Vector3(moveSpeed, 0f, 0f);
    }
    /// <summary>Makes the player move the direction it's facing a tile</summary>
    void MoveForward()
    {
        if (movePoint.rotation.z == 0)
        {
            MoveUp();
        }
        else if (Math.Round(movePoint.rotation.w, 2) == -0.71)
        {
            MoveRight();
        }
        else if (movePoint.rotation.z == 1)
        {
            MoveDown();
        }
        else if (Math.Round(movePoint.rotation.w, 2) == 0.71)
        {
            MoveLeft();
        }
    }

    // Directions
    void FaceUp()
    {
        movePoint.rotation = Quaternion.Euler(movePoint.rotation.x, movePoint.rotation.y, 0f);
    }
    void FaceRight()
    {
        movePoint.rotation = Quaternion.Euler(movePoint.rotation.x, movePoint.rotation.y, 270f);
    }
    void FaceDown()
    {
        movePoint.rotation = Quaternion.Euler(movePoint.rotation.x, movePoint.rotation.y, 180f);
    }
    void FaceLeft()
    {
        movePoint.rotation = Quaternion.Euler(movePoint.rotation.x, movePoint.rotation.y, 90f);
    }
}
