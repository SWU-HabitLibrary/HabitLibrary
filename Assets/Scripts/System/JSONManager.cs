using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONManager
{
    // �����͸� JSON���� ����&���� Ŭ����
    public T LoadData<T>(string _fileName) where T : new()
    {
        // �����͸� �ε��ϴ� �Լ�
        // T�� �����ڸ� ���� Ŭ������� ���׸� �Ű����� ���� �߰�

        string savedPath = GetPath(_fileName);    // ���� ���� ���

        if (!File.Exists(savedPath))              // ������ �������� �ʴ´ٸ� ����
        {
            Debug.Log("������ �������� �ʾƼ� ���� �� ����");

            T data = new T();
            SaveData<T>(_fileName, data);
            return data;
        }
        else                                     // ������ �����Ѵٸ� �ε�
        {
            string data = File.ReadAllText(savedPath);
            T t = JsonUtility.FromJson<T>(data);
            return t;
        }
    }

    public List<T> LoadDataList<T>(string _fileName) where T : new()
    {
        // ������ ����Ʈ�� �ε��ϴ� �Լ�
        // T�� �����ڸ� ���� Ŭ������� ���׸� �Ű����� ���� �߰�

        string savedPath = GetPath(_fileName);   // ���� ���� ���

        if (!File.Exists(savedPath))             // ������ �������� �ʴ´ٸ� ����
        {
            Debug.Log("������ �������� �ʾƼ� ���� �� ����");

            List<T> dataList = new List<T>();
            SaveDataList<T>(_fileName, dataList);

            return dataList;
        }
        else                                     // ������ �����Ѵٸ� �ε�
        {
            string data = File.ReadAllText(savedPath);
            return JsonUtility.FromJson<Wrapper<T>>(data).datalist;
        }
    }

    public void SaveData<T>(string _fileName, T _data)
    {
        // �����͸� Json���� �����ϴ� �Լ�

        string savedPath = GetPath(_fileName);
        string data = JsonUtility.ToJson(_data, true);

        File.WriteAllText(savedPath, data);
    }

    public void SaveDataList<T>(string _fileName, List<T> _dataList)
    {
        // ������ ����Ʈ�� Json���� �����ϴ� �Լ�

        string savedPath = GetPath(_fileName);

        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.datalist = _dataList;
        string data = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(savedPath, data);
    }

    private string GetPath(string _fileName)
    {
        // ���� ���� ���

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