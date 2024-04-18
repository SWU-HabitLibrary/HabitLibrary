using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class tempText : MonoBehaviour
{
    // �ؽ�Ʈ ��� �׽�Ʈ���� Ȯ���� �ӽ� �ؽ�Ʈ

    public T DataLoadText<T>(string _fileName)
    {
        //Json �����͸� �ҷ����� �Լ�
        try
        {
            string savePath = getPath(_fileName);    //���� ���� ���
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
