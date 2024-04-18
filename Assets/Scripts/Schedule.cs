using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Schedule
{
    // ������

    public long id;  //������ id (������ number)
    public string content;  // ������ ����
    public string date; // ������ �ۼ� ��¥
    public string time; // ������ �ۼ� �ð�
    public bool isCompleted;  // �Ϸ� ����

    public Schedule()
    {
        id = 0;
        content = date = time = "";
        isCompleted = false;
    }

    public Schedule(long _id,  string _content, string _date, string _time, bool _isCompleted)
    {
        id = _id;   
        content = _content;
        date = _date;
        time = _time;
        isCompleted = _isCompleted;        
    }
}
