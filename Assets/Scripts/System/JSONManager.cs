using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONManager
{
    // 데이터를 JSON으로 저장&관리 클래스

    public bool CheckDataExists(string fileName)
    {
        string filePath = GetFilePath(fileName);
        return File.Exists(filePath);
    }

    public T LoadData<T>(string fileName) where T : new()
    {
        // 데이터를 로드하는 함수
        // T가 생성자를 가진 클래스라는 제네릭 매개변수 제약 추가

        string filePath = GetFilePath(fileName);

        if (!File.Exists(filePath))
        {
            Debug.Log("파일이 존재하지 않아서 생성 및 저장");

            T newData = new T();
            SaveData(fileName, newData);
            return newData;
        }

        string json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<T>(json);
    }

    public List<T> LoadDataList<T>(string fileName) where T : new()
    {
        // 데이터 리스트를 로드하는 함수
        // T가 생성자를 가진 클래스라는 제네릭 매개변수 제약 추가

        string filePath = GetFilePath(fileName);

        if (!File.Exists(filePath))      
        {
            Debug.Log("파일이 존재하지 않아서 생성 및 저장");

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
        // 데이터를 Json으로 저장하는 함수

        string filePath = GetFilePath(fileName);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }

    public void SaveDataList<T>(string fileName, List<T> dataList)
    {
        // 데이터 리스트를 Json으로 저장하는 함수

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