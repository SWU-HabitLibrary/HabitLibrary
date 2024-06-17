using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScheduleItem : MonoBehaviour
{
    // 스케줄 아이템 스크립트

    public long scheduleId; // 스케줄 고유 번호

    public void Start()
    {
        InitializeButtons();
    }

    // 버튼 초기화 메서드
    private void InitializeButtons()
    {
        this.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => UpdateSchedule(ProgressType.Successful));
        this.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => UpdateSchedule(ProgressType.Failed));
    }

    // 스케줄 업데이트 메서드
    private void UpdateSchedule(ProgressType progressType)
    {
        Character character = GameManager.Instance.curCharacter;

        // 랜덤으로 스탯 선택 및 변경
        List<System.Action> statModifiers = new List<System.Action>()
        {
            () => character.hp += GetRandomStatChange(progressType, 1, 10),
            () => character.mp += GetRandomStatChange(progressType, 1, 10),
            () => character.intelligence += GetRandomStatChange(progressType, 1, 10),
            () => character.gold += GetRandomStatChange(progressType, 100, 1000)
        };

        int randomIndex = Random.Range(0, statModifiers.Count);
        statModifiers[randomIndex]();

        // 캐릭터 데이터 저장
        GameManager.Instance.CharacterDatas[GameManager.Instance.CharacterDatas.Count - 1] = character;
        GameManager.Instance.jsonManager.SaveDataList(Constants.CharacterData, GameManager.Instance.CharacterDatas);

        // 성공 또는 실패 메시지 출력
        Debug.Log(progressType == ProgressType.Successful ? "스케줄 성공" : "스케줄 실패");

        // 스케줄 로그에 저장
        GameObject.FindWithTag("ScheduleManager").GetComponent<ScheduleManager>().SavedToScheduleLog(scheduleId, progressType);
        Destroy(this.gameObject);
    }

    // 스탯 변경 값 생성 메서드
    private int GetRandomStatChange(ProgressType progressType, int min, int max)
    {
        return progressType == ProgressType.Successful ? Random.Range(min, max + 1) : Random.Range(-max, 0);
    }
}
