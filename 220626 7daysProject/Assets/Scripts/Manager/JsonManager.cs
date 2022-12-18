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
        //이제 우리가 이전에 저장했던 데이터를 꺼내야한다
        SaveData gameData;
        string loadPath = Application.dataPath;
        string directory = "/UserData";
        string appender = "/SaveData";

        string dotJson = ".json";

        StringBuilder builder = new StringBuilder(loadPath);
        builder.Append(directory);
        //위까지는 세이브랑 똑같다
        //파일스트림을 만들어준다. 파일모드를 open으로 해서 열어준다. 다 구글링이다
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
            //세이브 파일이 있는경우

            FileStream stream = new FileStream(builder.ToString(), FileMode.Open);

            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Close();
            string jsonData = Encoding.UTF8.GetString(bytes);

            //텍스트를 string으로 바꾼다음에 FromJson에 넣어주면은 우리가 쓸 수 있는 객체로 바꿀 수 있다
            gameData = JsonUtility.FromJson<SaveData>(jsonData);
        }
        else
        {
            //세이브파일이 없는경우
            gameData = null;
            //    = new SaveDataClass();
            //gameData.AddMedicineBySymptom(medicineDataWrapper, Symptom.water, Symptom.fire);
        }
        return gameData;
        //이 정보를 게임매니저나, 로딩으로 넘겨주는 것이당
    }
}
