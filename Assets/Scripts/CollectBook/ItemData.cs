using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    // 아이템 데이터 클래스

    public int id;
    public int count;

    public ItemData()
    {
        id = count = 0;
    }

    public ItemData(int _id, int _count)
    {
        id = _id;
        count = _count;
    }
}
