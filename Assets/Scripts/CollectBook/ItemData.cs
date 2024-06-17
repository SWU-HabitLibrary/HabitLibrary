using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    // ������ ������ Ŭ����

    public int id;
    public int count;

    public ItemData()
    {
        id = 0;
        count = 0;
    }

    public ItemData(int id, int count)
    {
        this.id = id;
        this.count = count;
    }
}
