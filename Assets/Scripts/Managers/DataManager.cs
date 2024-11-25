using System.IO;
using Generic;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;

public class DataManager : Singleton<DataManager>
{
    // --- 게임 데이터 파일이름 설정 ("원하는 이름(영문).json") --- //
    string GameDataFileName = "SaveData-GameJam.json";

    // --- 저장용 클래스 변수 --- //

    // 불러오기
    public SaveData LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        // 저장된 게임이 있다면
        if (File.Exists(filePath))
        {
            // 저장된 파일 읽어오고 Json을 클래스 형식으로 전환해서 할당
            string fromJsonData = File.ReadAllText(filePath);
            SaveData data = JsonConvert.DeserializeObject<SaveData>(fromJsonData);
            Debug.Log("불러오기 완료");
            return data;
        }
        else
        {
            Debug.LogError("파일이 존재하지 않음");
            SaveData data = new SaveData(0);//디폴트 Data
            SaveGameData(data);
            return data;
        }
    }


    // 저장하기
    public void SaveGameData(SaveData data)
    {
        // 클래스를 Json 형식으로 전환 (true : 가독성 좋게 작성)
        JsonSerializerSettings setting = new JsonSerializerSettings();
        setting.Formatting = Formatting.Indented;
        setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        // 이미 저장된 파일이 있다면 덮어쓰고, 없다면 새로 만들어서 저장
        File.WriteAllText(filePath, JsonConvert.SerializeObject(data, setting));
    }
}