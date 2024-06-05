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
        this.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => SeccessSchedule());    // 스케줄 성공 버튼 이벤트 추가
        this.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => FailSchedule()); // 스케줄 실패 버튼 이벤트 추가
    }

    public void SeccessSchedule()
    {
        // 스케줄 성공 함수

        GameManager.instance.playerCharacter.hp += 5;
        GameManager.instance.playerCharacter.gold += 100;
        GameManager.instance.GetComponent<CharacterStateJSON>().SaveToJson(GameManager.instance.playerCharacter);   // 저장
        Debug.Log("스케줄 성공");

        GameObject.FindWithTag("TopBar").GetComponent<CharacterState>().UpdateCharacterStates();    // 캐릭터 스텟 표시 업데이트*/
        GameObject.FindWithTag("ScheduleManager").GetComponent<ScheduleManager>().SavedToScheduleLog(scheduleId, ProgressType.Successful); //스케줄 로그 데이터로 이동
        Destroy(this.gameObject);
    }

    public void FailSchedule()
    {
        // 스케줄 실패 함수
        GameManager.instance.playerCharacter.hp -= 5;
        GameManager.instance.playerCharacter.gold -= 100;
        GameManager.instance.GetComponent<CharacterStateJSON>().SaveToJson(GameManager.instance.playerCharacter);   // 저장
        Debug.Log("스케줄 실패");

        GameObject.FindWithTag("ScheduleManager").GetComponent<ScheduleManager>().SavedToScheduleLog(scheduleId, ProgressType.Failed); //스케줄 로그 데이터로 이동
        Destroy(this.gameObject);
    }
}
