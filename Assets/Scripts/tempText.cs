using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class tempText : MonoBehaviour
{
    // 텍스트 출력 테스트에서 확인할 임시 텍스트

    public T DataLoadText<T>(string _fileName)
    {
        //Json 데이터를 불러오는 함수
        try
        {
            string savePath = getPath(_fileName);    //저장 파일 경로
            string loadJson = File.ReadAllText(savePath);

            T t = JsonUtility.FromJson<T>(loadJson);

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
