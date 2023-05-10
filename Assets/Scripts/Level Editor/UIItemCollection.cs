using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItemCollection : MonoBehaviour
{
    public GameObject SelectedPrefab { get; private set; }
    public bool EraserSelected { get; private set; }
    public Items SelectedItemType { get { return _selectedItem.itemType; } }
    private Item _selectedItem = new Item();

    UIItem _currentUIItem = null;
    UIItem _previousUIItem = null;
    private bool _firstTime = false;
    public void SelectItem(GameObject prefab)
    {
        DeselectPreviousItem();
        DeselectEraser();
        _selectedItem = prefab.GetComponent<Item>();
        SelectedPrefab = prefab;
        _previousUIItem = _currentUIItem;

        Debug.Log("Selected Item/Prefab: " + prefab.ToString());
        Debug.Log(prefab.GetComponent<Item>().itemType);
    }
    public void SelectEraser()
    {
        EraserSelected = true;
        Debug.Log("Eraser Selected");
    }
    void DeselectEraser()
    {
        EraserSelected = false;
        Debug.Log("Eraser Deselected");
    }
    void DeselectPreviousItem()
    {
        if (_firstTime)
        {
            _previousUIItem.DeleteBorder();
        }
        else
        {
            _firstTime = true;
        }
    }
    public void SetUIItem(UIItem uiItem)
    {
        _currentUIItem = uiItem;
    }
}
