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
        InitializeButtons();
    }

    // ��ư �ʱ�ȭ �޼���
    private void InitializeButtons()
    {
        this.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => UpdateSchedule(ProgressType.Successful));
        this.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => UpdateSchedule(ProgressType.Failed));
    }

    // ������ ������Ʈ �޼���
    private void UpdateSchedule(ProgressType progressType)
    {
        Character character = GameManager.Instance.curCharacter;

        // �������� ���� ���� �� ����
        List<System.Action> statModifiers = new List<System.Action>()
        {
            () => character.hp += GetRandomStatChange(progressType, 1, 10),
            () => character.mp += GetRandomStatChange(progressType, 1, 10),
            () => character.intelligence += GetRandomStatChange(progressType, 1, 10),
            () => character.gold += GetRandomStatChange(progressType, 100, 1000)
        };

        int randomIndex = Random.Range(0, statModifiers.Count);
        statModifiers[randomIndex]();

        // ĳ���� ������ ����
        GameManager.Instance.CharacterDatas[GameManager.Instance.CharacterDatas.Count - 1] = character;
        GameManager.Instance.jsonManager.SaveDataList(Constants.CharacterData, GameManager.Instance.CharacterDatas);

        // ���� �Ǵ� ���� �޽��� ���
        Debug.Log(progressType == ProgressType.Successful ? "������ ����" : "������ ����");

        // ������ �α׿� ����
        GameObject.FindWithTag("ScheduleManager").GetComponent<ScheduleManager>().SavedToScheduleLog(scheduleId, progressType);
        Destroy(this.gameObject);
    }

    // ���� ���� �� ���� �޼���
    private int GetRandomStatChange(ProgressType progressType, int min, int max)
    {
        return progressType == ProgressType.Successful ? Random.Range(min, max + 1) : Random.Range(-max, 0);
    }
}
