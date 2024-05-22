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
        // CombatItem 스크립터블 오브젝트 데이터를 업데이트하는 함수

        GameManager.instance.GetComponent<ScriptableObjectManager>().GetScriptableObjectToObjectList<CombatItem>(spreadSheetAddress, spreadSheetRange, spreadSheetWorksheet, (_loadedDataList) =>
        {
            dataList = _loadedDataList;
            GameManager.instance.GetComponent<ScriptableObjectManager>().SaveScriptableObjectAtPath(objectName);    // 변동사항 저장
            onUpdateComplete?.Invoke(); //onUpdateComplete 콜백 호출
        });
    }

    public void InitializeCombatItemData()
    {
        // CombatItem 스크립터블 오브젝트를 삭제하고 재생성하는 함수

        GameManager.instance.GetComponent<ScriptableObjectManager>().InitializeScriptableObject<CombatItemObject>(CreateInstance<CombatItemObject>(), objectName);
    }
}
