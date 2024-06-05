using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Schedule
{
    // ������

    public long id;           // ������ id
    public string content;    // ������ ����
    public string date;       // ������ �ۼ�(����) ��¥
    public string time;       // ������ �ۼ�(����) �ð�
    public ProgressType progressType;  // ������ ���� ����

    public Schedule()
    {
        id = 0;
        content = date = time = "";
        progressType = ProgressType.InProgress;
    }

    public Schedule(long _id,  string _content, string _date, string _time, ProgressType _progressType)
    {
        id = _id;   
        content = _content;
        date = _date;
        time = _time;
        progressType = _progressType;        
    }
}
public enum ProgressType
{
    InProgress,
    Successful,
    Failed
}
