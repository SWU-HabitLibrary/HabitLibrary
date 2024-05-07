using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectManager : MonoBehaviour
{
    // 스크립터블 오브젝트 관리 클래스


    private static string objectAddress_start = "Assets/Scripts/";
    private static string objectAddress_end = ".asset";


    public void InitializeScriptableObject<T>(UnityEngine.Object asset, string objectName)
    {
        // 스크립터블 오브젝트를 초기화하는 함수
        // 스프레드 시트 삭제 후 재생성

        string objectAddress = objectAddress_start + objectName + objectAddress_end;   // 스크립터블 오브젝트의 주소

        AssetDatabase.DeleteAsset(objectAddress);  // 기존 스크립터블 오브젝트 삭제
        AssetDatabase.CreateAsset(asset, objectAddress);   // 스크립터블 오브젝트 신규 생성
        AssetDatabase.SaveAssets(); // 에셋 저장
    }

    public void SaveScriptableObjectAtPath(string objectName)
    {
        // 해당 경로에 있는 스크립터블 오브젝트의 변동사항을 저장하는 함수

        string objectAddress = objectAddress_start + objectName + objectAddress_end;   // 스크립터블 오브젝트의 주소
        UnityEngine.Object scriptableObject = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(objectAddress);

        if (scriptableObject != null)   // 로드한 스크립터블 오브젝트가 null이 아니라면
        {
            EditorUtility.SetDirty(scriptableObject);   // 변동사항 저장
        }
    }

    public void GetScriptableObjectToObjectList<T>(string address, string range, long sheetID, Action<List<T>> onDataLoaded)
    {
        // 스프레드 시트에서 가져온 데이터를 스크립터블 오브젝트에 저장하는 함수
        // 스프레드 시트의 데이터 리스트를 가져옴

        this.GetComponent<SpreadSheetManager>().GetSpreadSheetDataToObject<T>(address, range, sheetID, (dataList) =>
        {
            onDataLoaded(dataList);
        });
    }
}
