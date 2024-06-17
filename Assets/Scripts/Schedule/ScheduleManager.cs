using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScheduleManager : MonoBehaviour
{
    // 스케줄을 생성하는 스크립트

    public GameObject scheduleTitle;         // 스케줄 이름칸
    public GameObject ScheduleContents;      // 스케줄 보드
    public GameObject[] ScheduleItem;        // 스케줄 아이템
    public GameObject ScheduleAddItem;       // 스케줄 추가 아이템
    public ScrollRect scrollRect;            // 스크롤 뷰의 ScrollRect 컴포넌트
    public GameObject EditPanel;             // 스케줄 편집 패널

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
        dataList = GameManager.Instance.jsonManager.LoadDataList<Schedule>(Constants.ScheduleData);

        if (dataList.Count < 0)
            return;

        foreach (var schedule in dataList.Select((value, index) => new { value, index }))
        {
            AddScheduleItem(schedule.value, schedule.index);
        }

        AdjustBottom();
    }

    private void AddScheduleItem(Schedule schedule, int index)
    {
        int randomNumber = UnityEngine.Random.Range(0, ScheduleItem.Length);
        GameObject newScheduleItem = Instantiate(ScheduleItem[randomNumber], ScheduleContents.transform); // 부모 지정하여 생성
        newScheduleItem.transform.GetChild(0).gameObject.GetComponent<Text>().text = schedule.content; // 텍스트 등록
        newScheduleItem.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => TouchEditButton(index)); // 스케줄 편집 버튼 이벤트 추가
        newScheduleItem.GetComponent<ScheduleItem>().scheduleId = schedule.id;
    }

    private void TouchWriteButton()
    {
        // 스케줄 작성창 버튼 함수
        GameObject newScheduleItem = Instantiate(ScheduleAddItem, ScheduleContents.transform); // 부모 지정하여 생성
        newScheduleItem.transform.SetAsFirstSibling(); // 첫번째 자식 오브젝트로 위치 변경
        newScheduleItem.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => TouchAddOfWriteButton(newScheduleItem)); // 스케줄 등록 버튼 이벤트 추가
        newScheduleItem.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => TouchCancleOfWriteButton(newScheduleItem)); // 스케줄 등록 취소 버튼 이벤트 추가

        AdjustBottom(); // 스크롤뷰 bottom 크기 조절
        ScrollToTop(); // 스크롤 뷰의 시점을 가장 위로 이동
    }

    private void TouchAddOfWriteButton(GameObject newScheduleItem)
    {
        // 스케줄 등록 버튼 함수
        string content = newScheduleItem.transform.GetChild(0).GetComponent<InputField>().text; // 입력한 스케줄 내용
        string id = GenerateScheduleId(); // 스케줄 id 생성
        ProgressType progressType = ProgressType.InProgress; // 완료 여부

        // 스케줄 아이템 추가
        Schedule newSchedule = new Schedule(long.Parse(id), content, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HHmmss"), progressType);
        dataList.Add(newSchedule); // json 추가
        GameManager.Instance.jsonManager.SaveDataList(Constants.ScheduleData, dataList); // 저장

        // 인게임 추가
        AddScheduleItem(newSchedule, dataList.Count - 1);

        Destroy(newScheduleItem);
    }

    private string GenerateScheduleId()
    {
        DateTime curDate = DateTime.Now;
        return curDate.ToString("yyyyMMddHHmmss");
    }

    private void TouchCancleOfWriteButton(GameObject newScheduleItem)
    {
        // 스케줄 등록 취소 버튼 함수
        Destroy(newScheduleItem);
        AdjustBottom();
    }

    public void SavedToScheduleLog(long scheduleId, ProgressType progressType)
    {
        // 완료된 스케줄을 로그 데이터로 저장하는 함수
        List<Schedule> savedDataList = GameManager.Instance.jsonManager.LoadDataList<Schedule>(Constants.ScheduleLogData);
        Schedule scheduleItem = dataList.FirstOrDefault(data => data.id == scheduleId);

        if (scheduleItem != null)
        {
            scheduleItem.progressType = progressType; // 진행 상태 변경
            savedDataList.Add(scheduleItem); // 로그 데이터 리스트에 추가
            GameManager.Instance.jsonManager.SaveDataList(Constants.ScheduleLogData, savedDataList); // 로그 데이터 리스트 저장

            dataList.Remove(scheduleItem); // 기존 데이터에서 삭제
            GameManager.Instance.jsonManager.SaveDataList(Constants.ScheduleData, dataList); // 저장
            AdjustBottom();
        }
    }

    private void TouchEditButton(int index)
    {
        // 스케줄 편집 버튼 클릭 함수
        EditPanel.SetActive(true);

        Debug.Log(index + "번째 스케줄 수정");
        RectTransform rectTransformScheduleItem = ScheduleContents.transform.GetChild(index).GetComponent<RectTransform>();
        RectTransform rectTransformEditPanel = EditPanel.transform.GetChild(0).GetComponent<RectTransform>();
        Vector3 newPosition = rectTransformEditPanel.anchoredPosition;
        newPosition.y = rectTransformScheduleItem.anchoredPosition.y - 330f;
        rectTransformEditPanel.anchoredPosition = newPosition;
    }

    private void DeleteSchedule(int index)
    {
        if (index >= 0 && index < dataList.Count)
        {
            dataList.RemoveAt(index);
            GameManager.Instance.jsonManager.SaveDataList(Constants.ScheduleData, dataList);
            UpdateScheduleData();
        }
    }

    private void ScrollToTop()
    {
        // 스크롤뷰의 시점을 가장 위로 이동
        scrollRect.verticalNormalizedPosition = 1f;
    }
}
