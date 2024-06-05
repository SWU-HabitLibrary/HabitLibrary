using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONManager
{
    // 데이터를 JSON으로 저장&관리 클래스
    public T LoadData<T>(string _fileName) where T : new()
    {
        // 데이터를 로드하는 함수
        // T가 생성자를 가진 클래스라는 제네릭 매개변수 제약 추가

        string savedPath = GetPath(_fileName);    // 파일 저장 경로

        if (!File.Exists(savedPath))              // 파일이 존재하지 않는다면 생성
        {
            Debug.Log("파일이 존재하지 않아서 생성 및 저장");

            T data = new T();
            SaveData<T>(_fileName, data);
            return data;
        }
        else                                     // 파일이 존재한다면 로드
        {
            string data = File.ReadAllText(savedPath);
            T t = JsonUtility.FromJson<T>(data);
            return t;
        }
    }

    public List<T> LoadDataList<T>(string _fileName) where T : new()
    {
        // 데이터 리스트를 로드하는 함수
        // T가 생성자를 가진 클래스라는 제네릭 매개변수 제약 추가

        string savedPath = GetPath(_fileName);   // 파일 저장 경로

        if (!File.Exists(savedPath))             // 파일이 존재하지 않는다면 생성
        {
            Debug.Log("파일이 존재하지 않아서 생성 및 저장");

            List<T> dataList = new List<T>();
            SaveDataList<T>(_fileName, dataList);

            return dataList;
        }
        else                                     // 파일이 존재한다면 로드
        {
            string data = File.ReadAllText(savedPath);
            return JsonUtility.FromJson<Wrapper<T>>(data).datalist;
        }
    }

    public void SaveData<T>(string _fileName, T _data)
    {
        // 데이터를 Json으로 저장하는 함수

        string savedPath = GetPath(_fileName);
        string data = JsonUtility.ToJson(_data, true);

        File.WriteAllText(savedPath, data);
    }

    public void SaveDataList<T>(string _fileName, List<T> _dataList)
    {
        // 데이터 리스트를 Json으로 저장하는 함수

        string savedPath = GetPath(_fileName);

        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.datalist = _dataList;
        string data = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(savedPath, data);
    }

    private string GetPath(string _fileName)
    {
        // 파일 저장 경로

#if UNITY_EDITOR
        return Application.dataPath + "/Saves/" + _fileName + ".json";
#elif UNITY_ANDROID
        return Application.persistentDataPath + "/Saves/" + _fileName + ".json";
#elif UNITY_IPHONE
        return Application.persistentDataPath + "/Saves/" + _fileName + ".json";
#else
        return Application.dataPath + "/Saves/" + _fileName + ".json";
#endif
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public List<T> datalist;
    }
}