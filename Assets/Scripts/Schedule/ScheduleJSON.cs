using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScheduleJSON : MonoBehaviour
{
    ScheduleData data;

    public void LoadScheduleData(string _fileName)
    {
        //데이터를 로드하는 함수

        string savePath = getPath(_fileName);    //저장 파일 경로

        if (!File.Exists(savePath))  //파일이 존재하지 않는다면
        {
            this.data = new ScheduleData();  //객체 생성
            DataSaveText(_fileName, data);    // 저장

            Debug.Log(_fileName + " 파일이 존재하지 않아서 생성");
        }
        else    //파일이 존재한다면
        {
            this.data = DataLoadText<ScheduleData>(_fileName);

            Debug.Log(_fileName + " 파일이 존재해서 로드");
        }
    }

    public ScheduleData GetScheduleData(string _fileName)
    {
        //데이터를 반환하는 함수

        LoadScheduleData(_fileName);
        return data;
    }

    public void DataSaveText<T>(string _fileName, T _data)
    {
        //데이터를 Json으로 저장하는 함수

        try
        {
            string savePath = getPath(_fileName);    //저장 파일 경로
            //string saveJson = JsonUtility.ToJson(_data, true);
            string saveJson = JsonUtility.ToJson(_data, true);

            File.WriteAllText(savePath, saveJson);

            Debug.Log("데이터 저장 완료" + _data.ToString());
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
        //Json 데이터를 불러오는 함수
        try
        {
            string savePath = getPath(_fileName);    //저장 파일 경로
            string loadJson = File.ReadAllText(savePath);

            T t = JsonUtility.FromJson<T>(loadJson);

            Debug.Log("로드 완료");
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
