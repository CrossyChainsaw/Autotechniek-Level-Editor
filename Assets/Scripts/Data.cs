using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class Data
{
    public class GridData
    {
        public const string DATA_FILE_GRID = "Grid.txt";

        public static async Task Overwrite(List<(int id, int x, int y)> data)
        {
            string[] newData = TupleListToStringArray(data);
            await File.WriteAllLinesAsync(DATA_FILE_GRID, newData);
        }
        public static List<(int id, int x, int y)> LoadAsTupleList()
        {
            List<(int id, int x, int y)> data = new List<(int id, int x, int y)>();
            List<int> singleObjectData = new List<int>();
            int count = 0;

            foreach (string line in System.IO.File.ReadLines(DATA_FILE_GRID))
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
        public static List<(int id, int x, int y)> ReadDataFromTextFile()
        {
            List<(int id, int x, int y)> data = new List<(int id, int x, int y)>();
            List<int> dataParts = new List<int>();
            int count = 0;

            foreach (string line in System.IO.File.ReadLines(DATA_FILE_GRID))
            {
                Debug.Log(line);
                dataParts.Add(Convert.ToInt32(line));
                count++;
                if (count == 3)
                {
                    data.Add((dataParts[0], dataParts[1], dataParts[2]));
                    dataParts.Clear();
                    count = 0;
                    // test this shit
                }
            }
            Debug.Log(data[0]);
            Debug.Log(data[1]);
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
        } // must be in string array to save in txt file
    }

    public class CarTaskData
    {
        const string DATA_FILE_CARTASKS = "CarTasks.txt";

        public static List<CarTask> LoadCarTasksFromTextFile()
        {
            List<CarTask> loadedCarTasks = new List<CarTask>();
            foreach (string line in System.IO.File.ReadLines("CarTasks.txt"))
            {
                int n = Convert.ToInt32(line);
                loadedCarTasks.Add(new CarTaskCollection().AllTasks[n]);
            }
            return loadedCarTasks;
        }
        /// <summary>Add new data entry to CarTaskData</summary>
        public static async Task AddDataEntry(int carTaskID)
        {
            List<CarTask> selectedTasks = LoadCarTasksFromTextFile();                    // load old data
            selectedTasks.Add(new CarTaskCollection().AllTasks[carTaskID]);              // add new entry
            string[] selectedTasksStringArray = CarTaskListToStringArray(selectedTasks); // format data
            await File.WriteAllLinesAsync(DATA_FILE_CARTASKS, selectedTasksStringArray); // write new data
        }
        /// <summary>Remove an existing data entry from CarTaskData</summary>
        public static async Task RemoveDataEntry(int carTaskID)
        {
            List<CarTask> selectedTasks = LoadCarTasksFromTextFile();
            selectedTasks = RemoveEntry(selectedTasks, carTaskID);
            string[] selectedTasksStringArray = CarTaskListToStringArray(selectedTasks); // format data
            await File.WriteAllLinesAsync(DATA_FILE_CARTASKS, selectedTasksStringArray); // write new data
            
            static List<CarTask> RemoveEntry(List<CarTask> selectedTasks, int carTaskID)
            {
                CarTask CarTaskThatHasToBeRemoved = null;
                foreach (CarTask c in selectedTasks)
                {
                    if (c.ID == carTaskID)
                    {
                        CarTaskThatHasToBeRemoved = c;
                    }
                }
                selectedTasks.Remove(CarTaskThatHasToBeRemoved);
                return selectedTasks;
            }
        }
        static string[] CarTaskListToStringArray(List<CarTask> carTaskList)
        {
            List<string> data = new List<string>();
            foreach (CarTask carTask in carTaskList)
            {
                data.Add(carTask.ID.ToString());
            }
            return data.ToArray();
        }
    }
}
