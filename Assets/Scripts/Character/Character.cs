using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    // Ä³¸¯ÅÍ ½ºÅÝ

    public string name;
    public string sex;
    public int hp;
    public int mp;
    public int stress;
    public int intelligence;
    public int gold;
    //public int gold { get; set; }

    public Character()
    {
        name = sex = "";
        hp = mp = stress = intelligence = 10;
        gold = 1000;
    }

    public Character(string _name, string _sex)
    {
        this.name = _name;
        this.sex = _sex;
        hp = mp = stress = intelligence = 10;
        gold = 1000;
    }
}
