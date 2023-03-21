using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class Data
{
    const string DATAFILE = "Editor.txt";
    public static async Task Overwrite(List<(int id, int x, int y)> data)
    {
        string[] newData = TupleListToStringArray(data);
        await File.WriteAllLinesAsync(DATAFILE, newData);
    }
    public static List<string> LoadAsStringList()
    {
        List<string> data = new List<string>();
        foreach (string line in File.ReadLines(DATAFILE))
        {
            data.Add(line);
        }
        return data;
    }
    public static List<(int id, int x, int y)> LoadAsTupleList()
    {
        List<(int id, int x, int y)> data = new List<(int id, int x, int y)>();
        List<int> singleObjectData = new List<int>();
        int count = 0;

        foreach (string line in System.IO.File.ReadLines(DATAFILE))
        {
            Debug.Log(line);
            singleObjectData.Add(Convert.ToInt32(line));
            count++;
            if (count == 3)
            {
                data.Add((singleObjectData[0], singleObjectData[1], singleObjectData[2]));
                singleObjectData.Clear();
                count = 0;
            }
        }
        return data;
    }
    private static string[] TupleListToStringArray(List<(int id, int x, int y)> data)
    {
        List<string> dataInStringList = new List<string>();
        foreach ((int id, int x, int y) entry in data)
        {
            dataInStringList.Add(entry.id.ToString());
            dataInStringList.Add(entry.x.ToString());
            dataInStringList.Add(entry.y.ToString());
        }
        string[] dataFinal = dataInStringList.ToArray();
        return dataFinal;
    }
}
