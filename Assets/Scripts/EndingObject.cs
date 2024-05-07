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
        // Ending 스크립터블 오브젝트 데이터를 업데이트하는 함수

        GameManager.instance.GetComponent<ScriptableObjectManager>().GetScriptableObjectToObjectList<Ending>(spreadSheetAddress, spreadSheetRange, spreadSheetWorksheet, (_loadedDataList) =>
        {
            dataList = _loadedDataList;
            GameManager.instance.GetComponent<ScriptableObjectManager>().SaveScriptableObjectAtPath(objectName);    // 변동사항 저장
            onUpdateComplete?.Invoke(); //onUpdateComplete 콜백 호출
        });
    }

    public void InitializeEndingData()
    {
        // Ending 스크립터블 오브젝트를 삭제하고 재생성하는 함수

        GameManager.instance.GetComponent<ScriptableObjectManager>().InitializeScriptableObject<EndingObject>(CreateInstance<EndingObject>(), objectName);
    }
}
