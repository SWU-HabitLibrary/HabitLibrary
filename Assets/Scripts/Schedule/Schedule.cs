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
        content = "";
        date = "";
        time = "";
        progressType = ProgressType.InProgress;
    }

    public Schedule(long id,  string content, string date, string time, ProgressType progressType)
    {
        this.id = id;
        this.content = content;
        this.date = date;
        this.time = time;
        this.progressType = progressType;
    }
}
public enum ProgressType
{
    InProgress,
    Successful,
    Failed
}
