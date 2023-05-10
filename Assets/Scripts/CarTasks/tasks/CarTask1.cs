using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Minigame1Tasks
{
    Task1,
    Task2,
    Task3,
    Task4,
    Task5
}

public class CarTask1 : MonoBehaviour
{
    int nBolts = 5;
    int nPreviousBolt = -1;
    public Minigame1Tasks currentTask = Minigame1Tasks.Task1;
    public GameObject mainWheel;
    public bool firstBoltTask4 = true;

    public void RemoveBolt()
    {
        nBolts--;
        if (nBolts == 0)
        {
            NextPhase();
        }
    }
    public void RemoveWheel()
    {
        NextPhase();
    }
    public void PlaceWheel()
    {
        NextPhase();
    }
    public void PlaceBolt()
    {
        nBolts++;
        if (nBolts == 5)
        {
            NextPhase();
        }
    }
    public void NextPhase()
    {
        int i = (int)currentTask;
        i++;
        currentTask = (Minigame1Tasks)i;
        Debug.Log(currentTask);
    }
    public void SwitchCamBackToGame()
    {
        GameObject MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        MainCamera.transform.position = new Vector3(0, 0, -10);
        GameObject Inventory = GameObject.FindGameObjectWithTag("Inventory");
        Inventory.transform.position = new Vector3(850, 0, 2);
    }
    public void Minigame_Item(int prefabID, GameObject gameObject)
    {
        UIItemCollection _uiItemCollection = GameObject.FindGameObjectWithTag("UIItemCollection").GetComponent<UIItemCollection>();

        if (prefabID == (int)Items.Bolt && currentTask == Minigame1Tasks.Task1 && _uiItemCollection.SelectedItemType == Items.CrossSocketWrench)
        {
            gameObject.SetActive(false);
            RemoveBolt();
        }
        else if (prefabID == (int)Items.Wheel && currentTask == Minigame1Tasks.Task2)
        {
            mainWheel = gameObject;
            gameObject.SetActive(false);
            RemoveWheel();
        }
        //else if (prefabID == (int)Items.Wheel && Minigame1.currentTask == Minigame1Tasks.Task3)
        //{
        //    mainWheel.SetActive(true);
        //    gameObject.SetActive(false);
        //    PlaceWheel();
        //}
        else if (prefabID == (int)Items.BoltHole && currentTask == Minigame1Tasks.Task4 && _uiItemCollection.SelectedItemType == Items.TorqueWrench)
        {
            // bolthole id 8
            string currentBolt = gameObject.transform.parent.gameObject.tag;
            int nCurrentBolt = System.Convert.ToInt32(currentBolt.Replace("Bolt ", ""));


            if (firstBoltTask4)
            {
                gameObject.SetActive(false);
                PlaceBolt();
                nPreviousBolt = nCurrentBolt;
                firstBoltTask4 = false;
            }
            // vanaf nu gaan we kijken of je kruislinks te werk gaat
            else
            {
                if (KruisLinksBoltCheck(nCurrentBolt, nPreviousBolt))
                {
                    gameObject.SetActive(false);
                    PlaceBolt();
                    nPreviousBolt = nCurrentBolt;
                }
                else
                {
                    Debug.Log("Je moet ze kruislinks erin schroeven met de momentsleutel");
                }
            }
        }
        else if (currentTask == Minigame1Tasks.Task5)
        {
            SwitchCamBackToGame();
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            player.EnableControls();
        }
        else
        {
            Debug.Log(prefabID);
        }
    }
    public void Minigame_UIItem(GameObject prefab, UIItem uiItem)
    {
        if (prefab.tag == "Wheel" && currentTask == Minigame1Tasks.Task3)
        {
            mainWheel.SetActive(true);
            Destroy(uiItem.gameObject);
            PlaceWheel();
        }
    }
    public void Activate()
    {
        // move cam
        GameObject MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        MainCamera.transform.position = new Vector3(0, -1500, 10);
        // move inventory
        GameObject InventoryObject = GameObject.FindGameObjectWithTag("Inventory");
        InventoryObject.transform.position = new Vector3(850, -1500, 15);
        // move buttonventory
        GameObject ButtonVentory = GameObject.FindGameObjectWithTag("Buttonventory");
        ButtonVentory.transform.position = new Vector3(-850, -1500, 15);
        // log
        Debug.Log("Activate Minigame");
    }
    bool KruisLinksBoltCheck(int nCurrentBolt, int nPreviousBolt)
    {
        if (System.Math.Abs(nCurrentBolt - nPreviousBolt) == 2 || System.Math.Abs(nCurrentBolt - nPreviousBolt) == 3)
        {
            return true; // indeed kruislinks
        }
        else
        {
            return false; // not kruislinks
        }
    }

}
