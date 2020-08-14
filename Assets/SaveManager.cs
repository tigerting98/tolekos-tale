using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Linq.Expressions;

public static class SaveManager
{
    public static SaveData current;
    public static void CreateNewData() {
        current = new SaveData();
        SaveData();
    }

    public static void SaveData() {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.dataPath + "/data.fun";
            FileStream stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, current);
            stream.Close();
        }
        catch (Exception ex) {
            Debug.Log(ex);
        }
    }

    public static SaveData LoadData() {
        string path = Application.dataPath + "/data.fun";
        if (current == null)
        {
            try
            {
                if (File.Exists(path))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    FileStream stream = new FileStream(path, FileMode.Open);
                    current = formatter.Deserialize(stream) as SaveData;
                    stream.Close();
                }
                else
                {

                    CreateNewData();

                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex);

            }
        }

        return current;
    }

        public static void UnlockNewStage(Difficulty difficulty, int stage)
        {
        SaveData data = LoadData();
        try
        {
            if (!data.acceessible[(int)difficulty, stage - 1])
            {
                data.acceessible[(int)difficulty, stage - 1] = true;
                current = data;
                SaveData();
            }
        }
        catch (Exception ex) {
            Debug.Log(ex);
        }
    }
    public static void ClearDifficulty(Difficulty difficulty) {
        try
        {
            SaveData data = LoadData();
            if (!data.cleared[(int)difficulty])
            {
                data.cleared[(int)difficulty] = true;
                current = data;
                SaveData();
            }
        }
        catch (Exception ex) {
            Debug.Log(ex);
        }
    }
    public static void Reset() {

        CreateNewData();
    }
    public static void UnlockAll() {
        SaveData data = LoadData();
        for (int i = 0; i < 5; i++) {
            data.cleared[i] = true;
            for (int j = 0; j < 6; j++) {
                data.acceessible[i, j] = true;
            }

        }
        current = data;
        SaveData();
        
    }
}

