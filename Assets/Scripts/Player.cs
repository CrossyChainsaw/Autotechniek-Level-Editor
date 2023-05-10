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
    Inventory _inventory;
    GameObject _inventoryGrid;
    List<CarTask> _taskList;

    private void Start()
    {
        _inventory = new Inventory();
        _inventoryGrid = GameObject.FindGameObjectWithTag("UIItemCollection");
        _taskList = GameObject.FindGameObjectWithTag("LoadSaveButton").GetComponent<LoadTasks>().CarTaskCollection.AllTasks; // links CarTaskCollection Constructor with player
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
                _inventory.AddItem(_inventoryGrid, col, item.ItemType);
            }

            // Wheel Col -> put inside wheel.cs? def not in player
            else if (item.PrefabID == (int)Items.Wheel)
            {
                if (_inventory.HasItem(_taskList[0].RequiredTools))
                {
                    new CarTask1().Activate();
                    DisableControls();
                }
                else
                {
                    print("You didn't collect the required tools. Required Tools: Cross Socket Wrench, Torque Wrench");
                }
            }
        }
    }

    // Inventory
    /// <summary>Removes a wheel from the player's inventory</summary>
    public void UseWheel()
    {
        _inventory.RemoveItem(Items.Wheel);
    }

    // Controls
    /// <summary>Disables the players controlls</summary>
    public void DisableControls()
    {
        GameModeManager.SetGamemode(Gamemodes.CarTask);
    }
    /// <summary>Enables the players controlls</summary>
    public void EnableControls()
    {
        GameModeManager.SetGamemode(Gamemodes.Play);
    }

    // Movement
    /// <summary>Moves the player in a direction using the arrow keys</summary>
    void Movement()
    {
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

    /// <summary>Makes the player move up a tile</summary>
    void MoveUp()
    {
        movePoint.position += new Vector3(0f, moveSpeed, 0f);
    }
    /// <summary>Makes the player move right a tile</summary>
    void MoveRight()
    {
        movePoint.position += new Vector3(moveSpeed, 0f, 0f);
    }
    /// <summary>Makes the player move down a tile</summary>
    void MoveDown()
    {
        movePoint.position -= new Vector3(0f, moveSpeed, 0f);
    }
    /// <summary>Makes the player move left a tile</summary>
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
    /// <summary>Makes the player face up</summary>
    void FaceUp()
    {
        movePoint.rotation = Quaternion.Euler(movePoint.rotation.x, movePoint.rotation.y, 0f);
    }
    /// <summary>Makes the player face right</summary>
    void FaceRight()
    {
        movePoint.rotation = Quaternion.Euler(movePoint.rotation.x, movePoint.rotation.y, 270f);
    }
    /// <summary>Makes the player face down</summary>
    void FaceDown()
    {
        movePoint.rotation = Quaternion.Euler(movePoint.rotation.x, movePoint.rotation.y, 180f);
    }
    /// <summary>Makes the player face left</summary>
    void FaceLeft()
    {
        movePoint.rotation = Quaternion.Euler(movePoint.rotation.x, movePoint.rotation.y, 90f);
    }
}
