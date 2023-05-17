using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItem : MonoBehaviour
{
    public Items item;
    public GameObject prefab;
    GameObject _border;
    UIItemCollection _uiItemCollection;
    CarTask1 _carTask1;

    private void Start()
    {
        _uiItemCollection = GameObject.FindGameObjectWithTag("UIItemCollection").GetComponent<UIItemCollection>();
    }

    private void OnMouseDown()
    {
        if (GameModeManager.Gamemode == Gamemodes.CarTask)
        {
            _carTask1 = (CarTask1)GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().TaskList[0];
            _carTask1.Minigame_UIItem(prefab, this);
        }
        _border = CreateBorder();
        _uiItemCollection.SetUIItem(this);
        _uiItemCollection.SelectItem(prefab);
    }
    GameObject CreateBorder()
    {
        GameObject border = Instantiate(Resources.Load("prefabs/border") as GameObject);
        border.transform.parent = gameObject.transform;
        border.transform.localPosition = Vector3.zero;
        return border;
    }
    public void DeleteBorder()
    {
        print("delete border");
        Destroy(_border);
    }
}
