using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedItem : MonoBehaviour
{
    //static public GameObject SelectedPrefab { get; set; }
    //static public bool Eraser { get; set; }
    //2static public Items ItemType { get { return _selectedItem.itemType; } }
    //static private Item _selectedItem = new Item();

    //static UIItem _currentUIItem = null;
    //static UIItem _previousUIItem = null;
    //static private bool _firstTime = false;
    public static void SelectItem(GameObject prefab)
    {
        DeselectPreviousItem();
        DeselectEraser();
        //_selectedItem = prefab.GetComponent<Item>();
        //SelectedPrefab = prefab;
        //_previousUIItem = _currentUIItem;

        Debug.Log("Selected: " + prefab.ToString());
        Debug.Log(prefab.GetComponent<Item>().ItemType);
    }
    public static void SelectEraser()
    {
        //Eraser = true;
        Debug.Log("Eraser Selected");
    }
    static void DeselectEraser()
    {
        //Eraser = false;
        Debug.Log("Eraser Deselected");
    }
    static void DeselectPreviousItem()
    {
        //if (//_firstTime)
        //{
            //_previousUIItem.DeleteBorder();
        //}
        //else
        //{
            //_firstTime = true;
        //}
    }
    static public void SetUIItem(UIItem u)
    {
        //_currentUIItem = u;
    }
}
