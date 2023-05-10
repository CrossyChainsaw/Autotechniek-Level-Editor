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
    public void AddItem(GameObject inventoryGrid, Collision2D col, Items item)
    {
        ItemList.Add(item);
        // Create itemslot at the correct place
        GameObject itemSlot = new GameObject("itemSlot", typeof(RectTransform));
        itemSlot.transform.parent = inventoryGrid.transform;
        // Add UI Item component to item slot
        itemSlot.AddComponent<UIItem>();
        UIItem itemSlot_UIItem = itemSlot.GetComponent<UIItem>();
        itemSlot_UIItem.item = item;
        itemSlot_UIItem.prefab = Resources.Load("prefabs/" + item.ToString()) as GameObject;
        // Add colliders
        itemSlot.AddComponent<BoxCollider2D>();
        BoxCollider2D itemSlot_BoxCollider2D = itemSlot.GetComponent<BoxCollider2D>();
        itemSlot_BoxCollider2D.offset = new Vector2(0, 0);
        itemSlot_BoxCollider2D.size = new Vector2(100, 100);
        // prefab object
        Items itemType = col.gameObject.GetComponent<Item>().ItemType;
        GameObject prefab = Resources.Load("prefabs/" + itemType.ToString()) as GameObject;
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
