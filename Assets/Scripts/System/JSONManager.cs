using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using UnityEngine;

public static class JSONManager
{
    // 데이터를 JSON으로 저장&관리 클래스

    public static List<T> FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return new List<T>(wrapper.Items);
    }

    public static string ToJson<T>(List<T> dataList)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = dataList.ToArray();
        return JsonUtility.ToJson(wrapper);
    }


    public static string ToJson<T>(List<T> array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array.ToArray();
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }

}
