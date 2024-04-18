using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScheduleManager : MonoBehaviour
{
    // 스케줄을 생성하는 스크립트

    public GameObject ScheduleContents; // 스케줄 보드
    public GameObject ScheduleItem; // 스케줄 아이템
    public InputField InputSchedule;    // 스케줄 입력창

    private static string fileName = "ScheduleData";   // 저장 파일 이름
    ScheduleData scheduleData;

    private void Start()
    {
        //scheduleData = new ScheduleData();
        UpdateScheduleData();   // 스케줄 업데이트
    }



    public void UpdateScheduleData()
    {
        // json에 저장된 스케줄 데이터를 업데이트 하는 함수

        scheduleData = this.gameObject.GetComponent<ScheduleJSON>().GetScheduleData(fileName);

        Debug.Log(scheduleData.curCount);


        for (int i = 0; i < scheduleData.curCount; i++)
        {
            Schedule curSchedule = scheduleData.dataList[i];

            GameObject newScheduleItem = Instantiate(ScheduleItem, ScheduleContents.transform); // 부모 지정하여 생성
            newScheduleItem.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = curSchedule.content;    // 텍스트 등록
        }

    }

    public void EnterSchedule()
    {
        // 스케줄을 등록 버튼을 누르면 스케줄을 추가하는 함수

        // 스케줄 데이터를 가져옴
        string content = InputSchedule.text;    // 입력한 스케줄 내용
        DateTime curDate = DateTime.Now;    // 현재 날짜 데이터
        string date = curDate.ToString("yyyyMMdd"); // 날짜
        string time = curDate.ToString("HHmm");   // 시간
        string id = date + time;    // 스케줄 id
        bool isCompleted = false;   // 완료 여부

        // 스케줄 아이템 추가
        Schedule newSchedule = new Schedule(long.Parse(id), content, date, time, isCompleted);
        scheduleData.dataList[scheduleData.curCount] = newSchedule;  // 추가
        Debug.Log("추가됨" + scheduleData.dataList[scheduleData.curCount].content);
        scheduleData.curCount++;

        GameObject newScheduleItem = Instantiate(ScheduleItem, ScheduleContents.transform); // 부모 지정하여 생성
        newScheduleItem.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = newSchedule.content;    // 텍스트 등록

        this.GetComponent<ScheduleJSON>().DataSaveText(fileName, scheduleData);

        // 
        InputSchedule.text = "";    //텍스트 초기화
    }
}
