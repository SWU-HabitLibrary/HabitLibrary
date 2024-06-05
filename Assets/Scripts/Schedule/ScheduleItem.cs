using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScheduleItem : MonoBehaviour
{
    // ������ ������ ��ũ��Ʈ

    public long scheduleId; // ������ ���� ��ȣ

    public void Start()
    {
        this.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => SeccessSchedule());    // ������ ���� ��ư �̺�Ʈ �߰�
        this.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => FailSchedule()); // ������ ���� ��ư �̺�Ʈ �߰�
    }

    public void SeccessSchedule()
    {
        // ������ ���� �Լ�

        GameManager.instance.playerCharacter.hp += 5;
        GameManager.instance.playerCharacter.gold += 100;
        GameManager.instance.GetComponent<CharacterStateJSON>().SaveToJson(GameManager.instance.playerCharacter);   // ����
        Debug.Log("������ ����");

        GameObject.FindWithTag("TopBar").GetComponent<CharacterState>().UpdateCharacterStates();    // ĳ���� ���� ǥ�� ������Ʈ*/
        GameObject.FindWithTag("ScheduleManager").GetComponent<ScheduleManager>().SavedToScheduleLog(scheduleId, ProgressType.Successful); //������ �α� �����ͷ� �̵�
        Destroy(this.gameObject);
    }

    public void FailSchedule()
    {
        // ������ ���� �Լ�
        GameManager.instance.playerCharacter.hp -= 5;
        GameManager.instance.playerCharacter.gold -= 100;
        GameManager.instance.GetComponent<CharacterStateJSON>().SaveToJson(GameManager.instance.playerCharacter);   // ����
        Debug.Log("������ ����");

        GameObject.FindWithTag("ScheduleManager").GetComponent<ScheduleManager>().SavedToScheduleLog(scheduleId, ProgressType.Failed); //������ �α� �����ͷ� �̵�
        Destroy(this.gameObject);
    }
}
