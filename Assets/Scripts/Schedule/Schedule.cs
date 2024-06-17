using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Schedule
{
    // 스케줄

    public long id;           // 스케줄 id
    public string content;    // 스케줄 내용
    public string date;       // 스케줄 작성(수정) 날짜
    public string time;       // 스케줄 작성(수정) 시간
    public ProgressType progressType;  // 스케줄 진행 과정

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
