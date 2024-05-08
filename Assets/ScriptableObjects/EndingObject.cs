using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Events;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "DataTable", menuName = "Scriptable Object Asset/Ending")]
public class EndingObject : ScriptableObject
{
    private static string spreadSheetAddress = "1_I2bQcbsrg2QX3YDs2uKgUMCBOACTpq8_avSr3gNgG8";
    private static long spreadSheetWorksheet = 0;
    private static string spreadSheetRange = "A2:B";
    private static string objectName = "Ending";

    public List<Ending> dataList = new List<Ending>();

    public void UpdateEndingData(Action onUpdateComplete)
    {
        // Ending ��ũ���ͺ� ������Ʈ �����͸� ������Ʈ�ϴ� �Լ�

        GameManager.instance.GetComponent<ScriptableObjectManager>().GetScriptableObjectToObjectList<Ending>(spreadSheetAddress, spreadSheetRange, spreadSheetWorksheet, (_loadedDataList) =>
        {
            dataList = _loadedDataList;
            GameManager.instance.GetComponent<ScriptableObjectManager>().SaveScriptableObjectAtPath(objectName);    // �������� ����
            onUpdateComplete?.Invoke(); //onUpdateComplete �ݹ� ȣ��
        });
    }

    public void InitializeEndingData()
    {
        // Ending ��ũ���ͺ� ������Ʈ�� �����ϰ� ������ϴ� �Լ�

        GameManager.instance.GetComponent<ScriptableObjectManager>().InitializeScriptableObject<EndingObject>(CreateInstance<EndingObject>(), objectName);
    }
}
