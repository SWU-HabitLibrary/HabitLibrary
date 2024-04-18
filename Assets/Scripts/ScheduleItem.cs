using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScheduleItem : MonoBehaviour
{
    // ������ ������ ��ũ��Ʈ

    int scheduleNumber; // ������ ���� ��ȣ

    public void CheckCompleteState()
    {
        // ������ üũ �Լ�

        if (this.transform.GetChild(0).GetComponent<Toggle>().isOn)
        {
            //this.GetComponent<Image>().color = Color.gray;
            this.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(true);
            GameManager.instance.playerCharacter.hp += 5;
            GameManager.instance.playerCharacter.gold += 100;

            GameManager.instance.GetComponent<CharacterStateJSON>().SaveToJson(GameManager.instance.playerCharacter);   // ����
            GameObject.FindWithTag("TopBar").GetComponent<CharacterState>().UpdateCharacterStates();    // ĳ���� ���� ǥ�� ������Ʈ
        }
        else
        {
            this.GetComponent<Image>().color = Color.white;
        }
    }

    public void DeleteScheduleItem()
    {
        // ������ X ��ư�� ������ ����

        Destroy(this.gameObject);
    }

}
