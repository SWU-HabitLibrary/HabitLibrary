using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScheduleData
{
    public int tempCount;
    public int curCount;
    public Schedule[] dataList;

    public ScheduleData() 
    {
        ResetData();
    }

    public void ResetData()
    {
        tempCount = 50;
        curCount = 0;
        dataList = new Schedule[tempCount];    // �ӽ÷� 50�� ����. List�� ������ ����

/*        DateTime curDate = DateTime.Now;    // ���� ��¥ ������
        string date = curDate.ToString("yyyyMMdd"); // ��¥
        string time = curDate.ToString("HHmm");   // �ð�
        string id = date + time;    // ������ id
        bool isCompleted = false;   // �Ϸ� ����

        // ������ ������ �߰�

        for (int i = 0; i < 5; i++) 
        {
            Schedule newSchedule = new Schedule(long.Parse(id), "�׽�Ʈ" + i, date, time, isCompleted);
            dataList.Add(newSchedule);
        }*/
    }
}
