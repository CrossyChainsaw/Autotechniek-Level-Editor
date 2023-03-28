using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSaveButton : MonoBehaviour
{
    public Button loadSaveButtonClick;
    public List<GameObject> prefabList = new List<GameObject>();

    void Start()
    {
        Button btn = loadSaveButtonClick.GetComponent<Button>();
        btn.onClick.AddListener(ButtonClick);
    }
    void ButtonClick()
    {
        LoadObjectsFromSave();
        DisableButton();
    }
    void DisableButton()
    {
        GetComponent<Button>().interactable = false;
    }
    void LoadObjectsFromSave()
    {
        List<(int id, int x, int y)> data = Data.LoadAsTupleList();
        while (data.Count > 0)
        {
            GameObject prefab = FindPrefabByID(data[0].id);
            Vector3 pos = new Vector3(data[0].x, data[0].y);
            Instantiate(prefab, pos, new Quaternion());
            data.RemoveAt(0);
        }
    }
    GameObject FindPrefabByID(int id)
    {
        GameObject correctPrefab = null;
        foreach (GameObject prefab in prefabList)
        {
            if (prefab.GetComponent<Item>().prefabID == id)
            {
                correctPrefab = prefab;
                break; // if this method fucks up, remove this line
            }
        }
        return correctPrefab;
    }
}
