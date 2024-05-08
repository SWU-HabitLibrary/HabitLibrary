using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatItem
{
    public int id;
    public string name;
    public string explanation;

    public CombatItem()
    {
        id = 0;
        name = explanation = "";
    }

    public CombatItem(int _id, string _name, string _explanation)
    {
        id = _id;
        name = _name;
        explanation = _explanation;
    }
}
