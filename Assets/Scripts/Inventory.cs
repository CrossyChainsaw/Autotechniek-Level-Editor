using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Items> ItemList { get; private set; }
    public Inventory()
    {
        ItemList = new List<Items>();
    }
    public void AddItem(Item item)
    {
        ItemList.Add(item.ItemType);
        // Create itemslot at the correct place
        GameObject itemSlot = new GameObject("itemSlot", typeof(RectTransform));
        itemSlot.transform.parent = GameObject.FindGameObjectWithTag("UIItemCollection").transform;
        // Add UI Item component to item slot
        itemSlot.AddComponent<UIItem>();
        UIItem itemSlot_UIItem = itemSlot.GetComponent<UIItem>();
        itemSlot_UIItem.item = item.ItemType;
        itemSlot_UIItem.prefab = Resources.Load("prefabs/" + item.ItemType.ToString()) as GameObject;
        // Add colliders
        itemSlot.AddComponent<BoxCollider2D>();
        BoxCollider2D itemSlot_BoxCollider2D = itemSlot.GetComponent<BoxCollider2D>();
        itemSlot_BoxCollider2D.offset = new Vector2(0, 0);
        itemSlot_BoxCollider2D.size = new Vector2(100, 100);
        // prefab object
        Debug.Log("item.ItemType.ToString(): " + item.ItemType.ToString());
        GameObject prefab = Resources.Load("prefabs/" + item.ItemType.ToString()) as GameObject; // empty for some reason
        GameObject objectWithPrefab = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        objectWithPrefab.transform.parent = itemSlot.transform;
    }
    public bool HasItem(params Items[] itemArray) // to prevent no parameter, put a parameter before it
    {
        foreach (Items item in itemArray)
        {
            if (!ItemList.Contains(item))
            {
                return false;
            }
        }
        return true;
    }
    public void RemoveItem(Items item)
    {
        if (ItemList.Contains(item))
        {
            ItemList.Remove(item);
        }
        else
        {
            Debug.Log("Player does not own: " + item);
        }
    }
}
