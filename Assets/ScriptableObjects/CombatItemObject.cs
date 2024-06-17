using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Events;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "DataTable", menuName = "Scriptable Object Asset/CombatItem")]
public class CombatItemObject : ScriptableObject
{

    private static string spreadSheetAddress = "16PoscIM42RF6yYUzTUV820FSRDbwENgn5JS9fQDwrZQ";
    private static long spreadSheetWorksheet = 0;
    private static string spreadSheetRange = "A2:D";
    private static string objectName = "CombatItem";

    public List<CombatItem> dataList = new List<CombatItem>();

    public void UpdateCombatItemData(Action onUpdateComplete)
    {
        // CombatItem ��ũ���ͺ� ������Ʈ �����͸� ������Ʈ�ϴ� �Լ�

        GameManager.Instance.GetComponent<ScriptableObjectManager>().GetScriptableObjectToObjectList<CombatItem>(spreadSheetAddress, spreadSheetRange, spreadSheetWorksheet, (_loadedDataList) =>
        {
            dataList = _loadedDataList;
            GameManager.Instance.GetComponent<ScriptableObjectManager>().SaveScriptableObjectAtPath(objectName);    // �������� ����
            onUpdateComplete?.Invoke(); //onUpdateComplete �ݹ� ȣ��
        });
    }

    public void InitializeCombatItemData()
    {
        // CombatItem ��ũ���ͺ� ������Ʈ�� �����ϰ� ������ϴ� �Լ�

        GameManager.Instance.GetComponent<ScriptableObjectManager>().InitializeScriptableObject<CombatItemObject>(CreateInstance<CombatItemObject>(), objectName);
    }
}
