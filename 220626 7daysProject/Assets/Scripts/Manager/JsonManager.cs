using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class JsonManager    //SH
{
    //세이브데이터를 세이브해줌.
    public void SaveJson(SaveData saveData, int index)
    {
        string jsonText;

        string savePath = Application.dataPath;
        string appender = "/userData/";
        string nameString = "SaveData";
        string dotJson = ".json";

        StringBuilder builder = new StringBuilder(savePath);
        builder.Append(appender);
        if (!Directory.Exists(builder.ToString()))
        {
            //디렉토리가 없는경우 만들어준다
            Directory.CreateDirectory(builder.ToString());

        }
        builder.Append(nameString);
        builder.Append(index.ToString());
        builder.Append(dotJson);

        jsonText = JsonUtility.ToJson(saveData, true);

        FileStream fileStream = new FileStream(builder.ToString(), FileMode.Create);
        byte[] bytes = Encoding.UTF8.GetBytes(jsonText);
        fileStream.Write(bytes, 0, bytes.Length);
        fileStream.Close();
    }

    public T ResourceDataLoad<T>(string name)
    {
        T gameData;

        string directory = "JsonData/";

        string appender1 = name;
        StringBuilder builder = new StringBuilder(directory);
        builder.Append(appender1);

        TextAsset jsonString = Resources.Load<TextAsset>(builder.ToString());
        
        gameData = JsonUtility.FromJson<T>(jsonString.ToString());

        return gameData;
    }

    public void SaveJson<T>(T saveData, string name)
    {
        string jsonText;

        string savePath = Application.dataPath;
        string appender = "/userData/";
        string nameString = name + ".json";

        StringBuilder builder = new StringBuilder(savePath);
        builder.Append(appender);
        if (!Directory.Exists(builder.ToString()))
        {
            //디렉토리가 없는경우 만들어준다
            Debug.Log("뭐야");
            Directory.CreateDirectory(builder.ToString());

        }
        builder.Append(nameString);

        jsonText = JsonUtility.ToJson(saveData, true);

        FileStream fileStream = new FileStream(builder.ToString(), FileMode.Create);
        byte[] bytes = Encoding.UTF8.GetBytes(jsonText);
        fileStream.Write(bytes, 0, bytes.Length);
        fileStream.Close();
    }
    public SaveData LoadSaveData()
    {
        SaveData gameData;
        string loadPath = Application.dataPath;
        string directory = "/UserData";
        string appender = "/SaveData";

        string dotJson = ".json";

        StringBuilder builder = new StringBuilder(loadPath);
        builder.Append(directory);

        string builderToString = builder.ToString();
        if (!Directory.Exists(builderToString))
        {
            //디렉토리가 없는경우 만들어준다
            Directory.CreateDirectory(builderToString);

        }
        builder.Append(appender);
        builder.Append(dotJson);

        if (File.Exists(builder.ToString()))
        {
            FileStream stream = new FileStream(builder.ToString(), FileMode.Open);

            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Close();
            string jsonData = Encoding.UTF8.GetString(bytes);

            gameData = JsonUtility.FromJson<SaveData>(jsonData);
        }
        else
        {
            //세이브파일이 없는경우
            gameData = null;
        }
        return gameData;
    }

    public QuestSaveData LoadQuestSaveData()
    {
        QuestSaveData gameData;
        string loadPath = Application.dataPath;
        string directory = "/UserData";
        string appender = "/QuestSaveData";

        string dotJson = ".json";

        StringBuilder builder = new StringBuilder(loadPath);
        builder.Append(directory);

        string builderToString = builder.ToString();
        if (!Directory.Exists(builderToString))
        {
            Directory.CreateDirectory(builderToString);

        }
        builder.Append(appender);
        builder.Append(dotJson);

        if (File.Exists(builder.ToString()))
        {
            FileStream stream = new FileStream(builder.ToString(), FileMode.Open);

            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Close();
            string jsonData = Encoding.UTF8.GetString(bytes);

            gameData = JsonUtility.FromJson<QuestSaveData>(jsonData);
        }
        else
        {
            //세이브파일이 없는경우
            gameData = null;
        }
        return gameData;
    }
}
