using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class ScheduleManager : MonoBehaviour
{
    // 스케줄을 생성하는 스크립트

    public GameObject scheduleTitle;   // 스케줄 이름칸
    public GameObject ScheduleContents; // 스케줄 보드
    public GameObject[] ScheduleItem; // 스케줄 아이템
    public GameObject ScheduleAddItem; // 스케줄 추가 아이템
    public GameObject Btn_Add;  // 스케줄 추가 아이콘
    public ScrollRect scrollRect; // 스크롤 뷰의 ScrollRect 컴포넌트

    private static string fileName = "ScheduleData";   // 저장 파일 이름
    private static string fileName_saved = "ScheduleLog";   // 스케줄 로그 데이터 이름
    private List<Schedule> dataList = new List<Schedule>();

    private void Start()
    {
        UpdateScheduleTitle();
        UpdateScheduleData();   // 스케줄 업데이트
    }

    private void UpdateScheduleTitle()
    {
        // 현재 날짜에 맞게 스케줄 이름 업데이트
        DateTime curDate = DateTime.Now;    // 현재 날짜 데이터
        CultureInfo english = new CultureInfo("en-US"); // 영어 정보

        string month = curDate.ToString("MMM", english); // 현재 월을 영어로 가져옴
        string day = curDate.ToString("dd"); // 현재 일을 가져옴
        string dayWithSuffix = GetDayWithSuffix(curDate.Day); // 일을 접미사와 함께 가져옴

        scheduleTitle.GetComponent<Text>().text = "Today is    " + month + "    " + dayWithSuffix + " !    ( • ᴗ - ) ✧";
    }

    private string GetDayWithSuffix(int day)
    {
        if (day >= 11 && day <= 13)
        {
            return day + "th";
        }
        switch (day % 10)
        {
            case 1:
                return day + "st";
            case 2:
                return day + "nd";
            case 3:
                return day + "rd";
            default:
                return day + "th";
        }
    }

    private void ClearChildObjects()
    {
        // ScheduleContents의 모든 자식 오브젝트 삭제
        foreach (Transform child in ScheduleContents.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void AdjustBottom()
    {
        // 스케줄 contents의 bottom(스크롤뷰 bottom 크기 조절 함수)
        RectTransform contentsRectTransform = ScheduleContents.GetComponent<RectTransform>();
        float childHeight = 0f;
        float childPadding = 30f;

        // 자식 오브젝트의 높이 가져오기
        if (ScheduleContents.transform.childCount > 0)
        {
            childHeight = ScheduleContents.transform.GetChild(0).GetComponent<RectTransform>().rect.height + childPadding;
        }

        int childCount = ScheduleContents.transform.childCount; // 자식 오브젝트 개수
        contentsRectTransform.sizeDelta = new Vector2(contentsRectTransform.sizeDelta.x, childHeight * childCount);
    }

    private void UpdateScheduleData()
    {
        // json에 저장된 스케줄 데이터를 업데이트 하는 함수

        ClearChildObjects();
        dataList = GameManager.instance.jsonManager.LoadDataList<Schedule>(fileName);

        if (dataList.Count < 0)
            return;

        for (int i = 0; i < dataList.Count; i++)
        {
            Schedule curSchedule = dataList[i];
            int randomNumber = UnityEngine.Random.Range(0, ScheduleItem.Length);

            GameObject newScheduleItem = Instantiate(ScheduleItem[randomNumber], ScheduleContents.transform); // 부모 지정하여 생성
            newScheduleItem.transform.GetChild(0).gameObject.GetComponent<Text>().text = curSchedule.content;    // 텍스트 등록
            newScheduleItem.GetComponent<ScheduleItem>().scheduleId = curSchedule.id;
        }

        AdjustBottom();
    }

    private void TouchWriteButton()
    {
        // 스케줄 작성창 버튼 함수

        GameObject newScheduleItem = Instantiate(ScheduleAddItem, ScheduleContents.transform); // 부모 지정하여 생성
        newScheduleItem.transform.SetAsFirstSibling();  // 첫번째 자식 오브젝트로 위치 변경
        newScheduleItem.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => TouchAddOfWriteButton(newScheduleItem));    // 스케줄 등록 버튼 이벤트 추가
        newScheduleItem.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => TouchCancleOfWriteButton(newScheduleItem)); // 스케줄 등록 취소 버튼 이벤트 추가

        AdjustBottom(); // 스크롤뷰 bottom 크기 조절
        ScrollToTop(); // 스크롤 뷰의 시점을 가장 위로 이동
    }

    private void TouchAddOfWriteButton(GameObject _newScheduleItem)
    {
        // 스케줄 등록 버튼 함수

        string content = _newScheduleItem.transform.GetChild(0).GetComponent<InputField>().text; // 입력한 스케줄 내용
        DateTime curDate = DateTime.Now;    // 현재 날짜 데이터
        string date = curDate.ToString("yyyyMMdd"); // 날짜
        string time = curDate.ToString("HHmmss");   // 시간
        string id = date + time;    // 스케줄 id
        ProgressType progressType = ProgressType.InProgress;   // 완료 여부



        // 스케줄 아이템 추가
        Schedule newSchedule = new Schedule(long.Parse(id), content, date, time, progressType);
        dataList.Add(newSchedule);  // json 추가
        GameManager.instance.jsonManager.SaveDataList(fileName, dataList);  // 저장

        // 인게임 추가
        int randomNumber = UnityEngine.Random.Range(0, ScheduleItem.Length);
        GameObject newScheduleItem = Instantiate(ScheduleItem[randomNumber], ScheduleContents.transform); // 부모 지정하여 생성
        newScheduleItem.GetComponent<ScheduleItem>().scheduleId = long.Parse(id);
        newScheduleItem.transform.GetChild(0).gameObject.GetComponent<Text>().text = content;    // 텍스트 등록

        Destroy(_newScheduleItem);
    }

    private void TouchCancleOfWriteButton(GameObject newSchedule)
    {
        // 스케줄 등록 취소 버튼 함수
        Destroy(newSchedule);
        AdjustBottom();
    }

    public void SavedToScheduleLog(long scheduleId, ProgressType progressType)
    {
        // 완료된 스케줄을 로그 데이터로 저장하는 함수

        // 로그 데이터에 저장
        List<Schedule> savedDataList = GameManager.instance.jsonManager.LoadDataList<Schedule>(fileName_saved);
        Schedule scheduleItem = dataList.FirstOrDefault(data => data.id == scheduleId);
        scheduleItem.progressType = progressType;           // 진행 상태 변경
        savedDataList.Add(scheduleItem);                    // 로그 데이터 리스트에 추가
        GameManager.instance.jsonManager.SaveDataList(fileName_saved, savedDataList);  // 로그 데이터 리스트 저장

        // 기존 데이터에서 삭제
        for (int i = 0; i < dataList.Count; i++)
        {
            if (dataList[i].id == scheduleId)
            {
                dataList.RemoveAt(i);
                break;
            }
        }
        GameManager.instance.jsonManager.SaveDataList(fileName, dataList);  // 저장
        AdjustBottom();
    }

    private void ScrollToTop()
    {
        // 스크롤뷰의 시점을 가장 위로 이동
        scrollRect.verticalNormalizedPosition = 1f;
    }
}
