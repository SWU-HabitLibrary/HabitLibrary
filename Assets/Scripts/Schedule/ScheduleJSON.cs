using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScheduleJSON : MonoBehaviour
{
    ScheduleData data;

    public void LoadScheduleData(string _fileName)
    {
        //�����͸� �ε��ϴ� �Լ�

        string savePath = getPath(_fileName);    //���� ���� ���

        if (!File.Exists(savePath))  //������ �������� �ʴ´ٸ�
        {
            this.data = new ScheduleData();  //��ü ����
            DataSaveText(_fileName, data);    // ����

            Debug.Log(_fileName + " ������ �������� �ʾƼ� ����");
        }
        else    //������ �����Ѵٸ�
        {
            this.data = DataLoadText<ScheduleData>(_fileName);

            Debug.Log(_fileName + " ������ �����ؼ� �ε�");
        }
    }

    public ScheduleData GetScheduleData(string _fileName)
    {
        //�����͸� ��ȯ�ϴ� �Լ�

        LoadScheduleData(_fileName);
        return data;
    }

    public void DataSaveText<T>(string _fileName, T _data)
    {
        //�����͸� Json���� �����ϴ� �Լ�

        try
        {
            string savePath = getPath(_fileName);    //���� ���� ���
            //string saveJson = JsonUtility.ToJson(_data, true);
            string saveJson = JsonUtility.ToJson(_data, true);

            File.WriteAllText(savePath, saveJson);

            Debug.Log("������ ���� �Ϸ�" + _data.ToString());
        }
        catch (FileNotFoundException e)
        {
            Debug.Log("The file was not found:" + e.Message);
        }
        catch (DirectoryNotFoundException e)
        {
            Debug.Log("The directory was not found: " + e.Message);
        }
        catch (IOException e)
        {
            Debug.Log("The file could not be opened:" + e.Message);
        }
    }

    public T DataLoadText<T>(string _fileName)
    {
        //Json �����͸� �ҷ����� �Լ�
        try
        {
            string savePath = getPath(_fileName);    //���� ���� ���
            string loadJson = File.ReadAllText(savePath);

            T t = JsonUtility.FromJson<T>(loadJson);

            Debug.Log("�ε� �Ϸ�");
            return t;
        }
        catch (FileNotFoundException e)
        {
            Debug.Log("The file was not found:" + e.Message);
        }
        catch (DirectoryNotFoundException e)
        {
            Debug.Log("The directory was not found: " + e.Message);
        }
        catch (IOException e)
        {
            Debug.Log("The file could not be opened:" + e.Message);
        }
        return default;
    }

    private static string getPath(string _fileName)
    {
#if UNITY_EDITOR
        return Application.dataPath + "/Saves/" + _fileName + ".json";
#elif UNITY_ANDROID
        return Application.persistentDataPath + "/Saves/" + fileName + ".json";
#elif UNITY_IPHONE
        return Application.persistentDataPath + "/Saves/" + fileName + ".json";
#else
        return Application.dataPath + "/Saves/" + fileName + ".json";
#endif
    }
}
