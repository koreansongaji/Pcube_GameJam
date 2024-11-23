using System.IO;
using Generic;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;

public class DataManager : Singleton<DataManager>
{
    // --- ���� ������ �����̸� ���� ("���ϴ� �̸�(����).json") --- //
    string GameDataFileName = "SaveData-GameJam.json";

    // --- ����� Ŭ���� ���� --- //

    // �ҷ�����
    public SaveData LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        // ����� ������ �ִٸ�
        if (File.Exists(filePath))
        {
            // ����� ���� �о���� Json�� Ŭ���� �������� ��ȯ�ؼ� �Ҵ�
            string fromJsonData = File.ReadAllText(filePath);
            SaveData data = JsonConvert.DeserializeObject<SaveData>(fromJsonData);
            Debug.Log("�ҷ����� �Ϸ�");
            return data;
        }
        else
        {
            Debug.LogError("������ �������� ����");
            SaveData data = new SaveData(0);//����Ʈ Data
            SaveGameData(data);
            return data;
        }
    }


    // �����ϱ�
    public void SaveGameData(SaveData data)
    {
        // Ŭ������ Json �������� ��ȯ (true : ������ ���� �ۼ�)
        JsonSerializerSettings setting = new JsonSerializerSettings();
        setting.Formatting = Formatting.Indented;
        setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        // �̹� ����� ������ �ִٸ� �����, ���ٸ� ���� ���� ����
        File.WriteAllText(filePath, JsonConvert.SerializeObject(data, setting));
    }
}