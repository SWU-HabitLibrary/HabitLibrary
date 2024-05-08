using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ending
{
    public int id;
    public string name;

    public Ending()
    {
        id = 0;
        name = "";
    }

    public Ending(int _id,  string _name)
    {
        id = _id;  
        name = _name;
    }
}
