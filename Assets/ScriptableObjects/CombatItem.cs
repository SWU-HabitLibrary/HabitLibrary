using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatItem
{
    public int id;
    public string name;
    public string explanation;
    public int price;

    public CombatItem()
    {
        id = price = 0;
        name = explanation = "";
    }

    public CombatItem(int _id, string _name, string _explanation, int _price)
    {
        id = _id;
        name = _name;
        explanation = _explanation;
        price = _price;
    }
}
