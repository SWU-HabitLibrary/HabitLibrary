using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Schedule
{
    // 스케줄

    public long id;  //스케줄 id (스케줄 number)
    public string content;  // 스케줄 내용
    public string date; // 스케줄 작성 날짜
    public string time; // 스케줄 작성 시간
    public bool isCompleted;  // 완료 여부

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
