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
        dataList = new Schedule[tempCount];    // 임시로 50개 설정. List로 수정할 예정

/*        DateTime curDate = DateTime.Now;    // 현재 날짜 데이터
        string date = curDate.ToString("yyyyMMdd"); // 날짜
        string time = curDate.ToString("HHmm");   // 시간
        string id = date + time;    // 스케줄 id
        bool isCompleted = false;   // 완료 여부

        // 스케줄 아이템 추가

        for (int i = 0; i < 5; i++) 
        {
            Schedule newSchedule = new Schedule(long.Parse(id), "테스트" + i, date, time, isCompleted);
            dataList.Add(newSchedule);
        }*/
    }
}
