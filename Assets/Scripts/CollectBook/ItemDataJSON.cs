using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemDataJSON : MonoBehaviour
{
    // 플레이어의 아이템(종류 무관) 데이터를 JSON으로 저장하는 스크립트
    string path = Application.dataPath + "/Saves/";

    private List<ItemData> dataList;

    public void Start()
    {
        /*        // 테스트를 위한 임시 데이터 생성
        dataList = new List<ItemData>();
        dataList.Add(new ItemData { id = 3000, count = 5 });
        dataList.Add(new ItemData { id = 3001, count = 2 });
        dataList.Add(new ItemData { id = 3003, count = 10 });
        dataList.Add(new ItemData { id = 3004, count = 9 });
        dataList.Add(new ItemData { id = 3005, count = 4 });
        dataList.Add(new ItemData { id = 3008, count = 5 });
        dataList.Add(new ItemData { id = 3009, count = 7 });
        dataList.Add(new ItemData { id = 3010, count = 1 });

        SaveToJson("CombatItemData");*/

        /*List<ItemData> dataList = new List<ItemData>();
        dataList.Add(new ItemData { id = 5001, count = 1 });
        dataList.Add(new ItemData { id = 5003, count = 2 });
        dataList.Add(new ItemData { id = 5004, count = 1 });
        SaveToJson("EndingData", dataList);*/
    }

    public List<ItemData> UpdateItemData(string _fileName)
    {
        dataList = LoadToJson(_fileName);

        return dataList;
    }

    public List<ItemData> LoadToJson(string _fileName)
    {
        List<ItemData> loadDataList = new List<ItemData>();

        string[] lines = File.ReadAllLines(path + _fileName + ".json");
        foreach (var line in lines)
        {
            loadDataList.Add(JsonUtility.FromJson<ItemData>(line));
        }

        return loadDataList;
    }

    public void SaveToJson(string _fileName, List<ItemData> _dataList)
    {
        foreach (var item in _dataList)
        {
            File.AppendAllText(path + _fileName + ".json", JsonUtility.ToJson(item) + "\n");
        }
    }
}

[System.Serializable]
public class DataListWrapper    
{
    // 리스트를 저장하기 위해 래핑
    public List<ItemData> dataList;
}