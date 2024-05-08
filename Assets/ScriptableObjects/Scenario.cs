using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Scenario
{
    public int id;
    public string content;

    public Scenario()
    {
        id = 0;
        content = "";
    }

    public Scenario(int _id,  string _content)
    {
        id = _id;
        content = _content;
    }
}
