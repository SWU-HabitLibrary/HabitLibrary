using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONManager
{
    // �����͸� JSON���� ����&���� Ŭ����

    public bool CheckDataExists(string fileName)
    {
        string filePath = GetFilePath(fileName);
        return File.Exists(filePath);
    }

    public T LoadData<T>(string fileName) where T : new()
    {
        // �����͸� �ε��ϴ� �Լ�
        // T�� �����ڸ� ���� Ŭ������� ���׸� �Ű����� ���� �߰�

        string filePath = GetFilePath(fileName);

        if (!File.Exists(filePath))
        {
            Debug.Log("������ �������� �ʾƼ� ���� �� ����");

            T newData = new T();
            SaveData(fileName, newData);
            return newData;
        }

        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<T>(json);
    }

    public List<T> LoadDataList<T>(string fileName) where T : new()
    {
        // ������ ����Ʈ�� �ε��ϴ� �Լ�
        // T�� �����ڸ� ���� Ŭ������� ���׸� �Ű����� ���� �߰�

        string filePath = GetFilePath(fileName);

        if (!File.Exists(filePath))      
        {
            Debug.Log("������ �������� �ʾƼ� ���� �� ����");

            List<T> newDataList = new List<T>();
            SaveDataList(filePath, newDataList);
            return newDataList;
        }

        string json = File.ReadAllText(filePath);
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.datalist;
    }

    public void SaveData<T>(string fileName, T data)
    {
        // �����͸� Json���� �����ϴ� �Լ�

        string filePath = GetFilePath(fileName);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }

    public void SaveDataList<T>(string fileName, List<T> dataList)
    {
        // ������ ����Ʈ�� Json���� �����ϴ� �Լ�

        string filePath = GetFilePath(fileName);

        Wrapper<T> wrapper = new Wrapper<T> { datalist = dataList };
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(filePath, json);
    }

    private string GetFilePath(string fileName)
    {
#if UNITY_EDITOR
        string basePath = Application.dataPath;
#elif UNITY_ANDROID || UNITY_IPHONE
        string basePath = Application.persistentDataPath;
#else
        string basePath = Application.dataPath;
#endif
        return Path.Combine(basePath, "Saves", $"{fileName}.json");
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public List<T> datalist;
    }
}