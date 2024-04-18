using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScheduleItem : MonoBehaviour
{
    // 스케줄 아이템 스크립트

    int scheduleNumber; // 스케줄 고유 번호

    public void CheckCompleteState()
    {
        // 스케줄 체크 함수

        if (this.transform.GetChild(0).GetComponent<Toggle>().isOn)
        {
            //this.GetComponent<Image>().color = Color.gray;
            this.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(true);
            GameManager.instance.playerCharacter.hp += 5;
            GameManager.instance.playerCharacter.gold += 100;

            GameManager.instance.GetComponent<CharacterStateJSON>().SaveToJson(GameManager.instance.playerCharacter);   // 저장
            GameObject.FindWithTag("TopBar").GetComponent<CharacterState>().UpdateCharacterStates();    // 캐릭터 스텟 표시 업데이트
        }
        else
        {
            this.GetComponent<Image>().color = Color.white;
        }
    }

    public void DeleteScheduleItem()
    {
        // 스케줄 X 버튼을 누르면 삭제

        Destroy(this.gameObject);
    }

}
