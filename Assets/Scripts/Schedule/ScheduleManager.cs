using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScheduleManager : MonoBehaviour
{
    // �������� �����ϴ� ��ũ��Ʈ

    public GameObject ScheduleContents; // ������ ����
    public GameObject ScheduleItem; // ������ ������
    public InputField InputSchedule;    // ������ �Է�â

    private static string fileName = "ScheduleData";   // ���� ���� �̸�
    ScheduleData scheduleData;

    private void Start()
    {
        //scheduleData = new ScheduleData();
        UpdateScheduleData();   // ������ ������Ʈ
    }



    public void UpdateScheduleData()
    {
        // json�� ����� ������ �����͸� ������Ʈ �ϴ� �Լ�

        scheduleData = this.gameObject.GetComponent<ScheduleJSON>().GetScheduleData(fileName);

        Debug.Log(scheduleData.curCount);


        for (int i = 0; i < scheduleData.curCount; i++)
        {
            Schedule curSchedule = scheduleData.dataList[i];

            GameObject newScheduleItem = Instantiate(ScheduleItem, ScheduleContents.transform); // �θ� �����Ͽ� ����
            newScheduleItem.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = curSchedule.content;    // �ؽ�Ʈ ���
        }

    }

    public void EnterSchedule()
    {
        // �������� ��� ��ư�� ������ �������� �߰��ϴ� �Լ�

        // ������ �����͸� ������
        string content = InputSchedule.text;    // �Է��� ������ ����
        DateTime curDate = DateTime.Now;    // ���� ��¥ ������
        string date = curDate.ToString("yyyyMMdd"); // ��¥
        string time = curDate.ToString("HHmm");   // �ð�
        string id = date + time;    // ������ id
        bool isCompleted = false;   // �Ϸ� ����

        // ������ ������ �߰�
        Schedule newSchedule = new Schedule(long.Parse(id), content, date, time, isCompleted);
        scheduleData.dataList[scheduleData.curCount] = newSchedule;  // �߰�
        Debug.Log("�߰���" + scheduleData.dataList[scheduleData.curCount].content);
        scheduleData.curCount++;

        GameObject newScheduleItem = Instantiate(ScheduleItem, ScheduleContents.transform); // �θ� �����Ͽ� ����
        newScheduleItem.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Text>().text = newSchedule.content;    // �ؽ�Ʈ ���

        this.GetComponent<ScheduleJSON>().DataSaveText(fileName, scheduleData);

        // 
        InputSchedule.text = "";    //�ؽ�Ʈ �ʱ�ȭ
    }
}
