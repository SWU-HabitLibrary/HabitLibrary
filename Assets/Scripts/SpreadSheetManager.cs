using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

public class SpreadSheetManager : MonoBehaviour
{
    // 스프레드 시트 데이터 매니저

    private static string defaultAddress = "https://docs.google.com/spreadsheets/d/"; // 스프레드 시트 주소의 공통 부분

    public void GetSpreadSheetDataToObject<T>(string address, string range, long sheetID, Action<List<T>> callback)
    {
        // 주소, 시트의 범위, 시트ID를 입력하면 스프레드 시트의 데이터를 객체 리스트로 반환하는 함수

        StartCoroutine(GetSpreadSheetDataToText<T>(address, range, sheetID, (www) =>
        {
            List<T> dataList = GetSpreadSheetDatas<T>(www.downloadHandler.text);
            callback(dataList); //데이터를 콜백으로 전달
        }));
    }

    public List<T> GetSpreadSheetDatas<T>(string data)
    {
        // 스트레드 시트 클래스 인스턴스화 한 데이터를 리스트화

        List<T> returnList = new List<T>();
        string[] splitedData = data.Split('\n');    // 행을 기준으로 분리

        foreach (string element in splitedData)
        {
            string[] datas = element.Split('\t');   // 열을 기준으로 분리
            returnList.Add(GetSpreadSheetData<T>(datas));   // 리스트에 추가
        }
        return returnList;
    }


    public T GetSpreadSheetData<T>(string[] datas)
    {
        // 스프레드 시트 텍스트 데이터 클래스 인스턴스화

        object data = Activator.CreateInstance(typeof(T));

        // 클래스에 있는 변수들을 순서대로 저장한 배열
        FieldInfo[] fields = typeof(T).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        for (int i = 0; i < datas.Length; i++)
        {
            try
            {
                // string > parse
                Type type = fields[i].FieldType;
                if (string.IsNullOrEmpty(datas[i])) continue;

                // 변수에 맞는 자료형으로 파싱해서 넣음
                if (type == typeof(int))
                    fields[i].SetValue(data, int.Parse(datas[i]));
                else if (type == typeof(float))
                    fields[i].SetValue(data, float.Parse(datas[i]));
                else if (type == typeof(bool))
                    fields[i].SetValue(data, bool.Parse(datas[i]));
                else if (type == typeof(string))
                    fields[i].SetValue(data, datas[i]);
                //enum이라면
                else
                    fields[i].SetValue(data, Enum.Parse(type, datas[i]));
            }
            catch (Exception e)
            {
                Debug.LogError($"스프레드 시트 파싱 에러 : {e.Message}");
            }
        }

        return (T)data;
    }

    public IEnumerator GetSpreadSheetDataToText<T>(string address, string range, long sheetID, Action<UnityWebRequest> callback)
    {
        // 주소, 시트의 범위, 시트ID를 입력하면 스프레드 시트의 데이터를 하나의 텍스트로 반환하는 함수

        UnityWebRequest www = UnityWebRequest.Get(GetTSVAddress(address, range, sheetID));
        yield return www.SendWebRequest();

        if (www != null)
        {
            callback(www);
        }
        www.Dispose();
    }

    public static string GetTSVAddress(string address, string range, long sheetID)
    {
        // 주소, 시트의 범위, 시트ID를 입력하면 TSV 주소로 반환하는 함수

        return defaultAddress + $"{address}/export?format=tsv&range={range}&grid={sheetID}";
    }
}
