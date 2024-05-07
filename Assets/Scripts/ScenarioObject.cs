using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Events;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "DataTable", menuName = "Scriptable Object Asset/Scenario")]
public class ScenarioObject : ScriptableObject
{
    private static string spreadSheetAddress = "1ybOIIyK6y6ZrQ_JMdIdR7TPaPJIIbb1es1zL7D7tvL0";
    private static long spreadSheetWorksheet = 0;
    private static string spreadSheetRange = "A2:B";
    private static string objectName = "Scenario";

    public List<Scenario> dataList = new List<Scenario>();

    public void UpdateScenarioData(Action onUpdateComplete)
    {
        // Scenario 스크립터블 오브젝트 데이터를 업데이트하는 함수

        GameManager.instance.GetComponent<ScriptableObjectManager>().GetScriptableObjectToObjectList<Scenario>(spreadSheetAddress, spreadSheetRange, spreadSheetWorksheet, (_loadedDataList) =>
        {
            dataList = _loadedDataList;
            GameManager.instance.GetComponent<ScriptableObjectManager>().SaveScriptableObjectAtPath(objectName);    // 변동사항 저장
            onUpdateComplete?.Invoke(); //onUpdateComplete 콜백 호출
        });
    }

    public void InitializeScenarioData()
    {
        // Scenario 스크립터블 오브젝트를 삭제하고 재생성하는 함수

        GameManager.instance.GetComponent<ScriptableObjectManager>().InitializeScriptableObject<ScenarioObject>(CreateInstance<ScenarioObject>(), objectName);
    }
}
