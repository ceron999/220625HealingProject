using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class DialogueParser : MonoBehaviour
{
    public Dialogue[] Parse(string csvFileName)
    {
       //현재 파싱을 원하는 파일의 위치를 찾기 위한 string 모음
       string directory = "CsvFiles/";
        
        //파일 위치+이름 을 builder에 저장합니다.
        StringBuilder builder = new StringBuilder(directory);

        builder.Append(csvFileName);

        List<Dialogue> dialogueList = new List<Dialogue>(); //대화 리스트 생성
        TextAsset csvData = Resources.Load<TextAsset>(builder.ToString()); //csv파일 가져오기
        
        string[] data = csvData.text.Split(new char[] { '\n' });    //\n에 따라 쪼갬

        for (int i = 1; i < data.Length - 1;)
        {
            string[] row = data[i].Split(new char[] { ',' });

            Dialogue dialogue = new Dialogue();
            
            dialogue.name = row[1];
            List<string> contextList = new List<string>();

            do
            {
                contextList.Add(row[2]);

                if (++i < data.Length - 1)
                    row = data[i].Split(new char[] { ',' });
                else break;

            } while (row[0].ToString() == "");

            dialogue.contexts = contextList.ToArray();
            
            dialogueList.Add(dialogue);
        }
        return dialogueList.ToArray();
    }
}
