using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectManager : MonoBehaviour
{
    // ��ũ���ͺ� ������Ʈ ���� Ŭ����


    private static string objectAddress_start = "Assets/Scripts/";
    private static string objectAddress_end = ".asset";


    public void InitializeScriptableObject<T>(UnityEngine.Object asset, string objectName)
    {
        // ��ũ���ͺ� ������Ʈ�� �ʱ�ȭ�ϴ� �Լ�
        // �������� ��Ʈ ���� �� �����

        string objectAddress = objectAddress_start + objectName + objectAddress_end;   // ��ũ���ͺ� ������Ʈ�� �ּ�

        AssetDatabase.DeleteAsset(objectAddress);  // ���� ��ũ���ͺ� ������Ʈ ����
        AssetDatabase.CreateAsset(asset, objectAddress);   // ��ũ���ͺ� ������Ʈ �ű� ����
        AssetDatabase.SaveAssets(); // ���� ����
    }

    public void SaveScriptableObjectAtPath(string objectName)
    {
        // �ش� ��ο� �ִ� ��ũ���ͺ� ������Ʈ�� ���������� �����ϴ� �Լ�

        string objectAddress = objectAddress_start + objectName + objectAddress_end;   // ��ũ���ͺ� ������Ʈ�� �ּ�
        UnityEngine.Object scriptableObject = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(objectAddress);

        if (scriptableObject != null)   // �ε��� ��ũ���ͺ� ������Ʈ�� null�� �ƴ϶��
        {
            EditorUtility.SetDirty(scriptableObject);   // �������� ����
        }
    }

    public void GetScriptableObjectToObjectList<T>(string address, string range, long sheetID, Action<List<T>> onDataLoaded)
    {
        // �������� ��Ʈ���� ������ �����͸� ��ũ���ͺ� ������Ʈ�� �����ϴ� �Լ�
        // �������� ��Ʈ�� ������ ����Ʈ�� ������

        this.GetComponent<SpreadSheetManager>().GetSpreadSheetDataToObject<T>(address, range, sheetID, (dataList) =>
        {
            onDataLoaded(dataList);
        });
    }
}
