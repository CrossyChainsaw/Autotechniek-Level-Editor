using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public Transform movePoint;
    public SpriteRenderer spriteRenderer;
    public LayerMask stopMovementLayer;
    GameObject _drillItemSlot;
    bool _hasDrill = false;

    private void Start()
    {
        _drillItemSlot = GameObject.FindGameObjectWithTag("DrillItemSlot");
    }
    void Update()
    {
        Movement();
    }

    // Collision/Interaction
    private void OnCollisionEnter2D(Collision2D col)
    {
        Item item = col.gameObject.GetComponent<Item>();
        Debug.Log(item.prefabID);

        if (item.collectable == true)
        {
            AddDrillToInventoryVisualy(col);
            AddDrillToInventory();
            Destroy(col.gameObject);
        }
        else if (item.prefabID == (int)Items.Wheel && _hasDrill)
        {
            GameObject MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            MainCamera.transform.position = new Vector3(0, -1500, 10);
            print("Activate Minigame");
            DisableControls();
        }
        else if (item.prefabID == (int)Items.GreenBlock)
        {
            spriteRenderer.color = new Color(0, 255, 0, 255);
        }
    }
    /// <summary>Checks if player is on a drill item</summary>

    // Inventory
    void AddDrillToInventoryVisualy(Collision2D col)
    {
        Debug.Log(col.gameObject.tag);
        var variableForPrefab = Resources.Load("prefabs/" + col.gameObject.tag) as GameObject;
        GameObject go = Instantiate(variableForPrefab, _drillItemSlot.transform);
        go.transform.localPosition = Vector3.zero;
    }
    void AddDrillToInventory()
    {
        _hasDrill = true;
    }


    // Other
    public void DisableControls()
    {
        GameModeManager.SetGamemode.Menu();
    }
    public void EnableControls()
    {
        GameModeManager.SetGamemode.Play();
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

    /// <summary>Makes the player move up</summary>
    void MoveUp()
    {
        movePoint.position += new Vector3(0f, moveSpeed, 0f);
    }
    /// <summary>Makes the player move right</summary>
    void MoveRight()
    {
        movePoint.position += new Vector3(moveSpeed, 0f, 0f);
    }
    /// <summary>Makes the player move down</summary>
    void MoveDown()
    {
        movePoint.position -= new Vector3(0f, moveSpeed, 0f);
    }
    /// <summary>Makes the player move left</summary>
    void MoveLeft()
    {
        movePoint.position -= new Vector3(moveSpeed, 0f, 0f);
    }
    /// <summary>Makes the player move the direction it's facing</summary>
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
