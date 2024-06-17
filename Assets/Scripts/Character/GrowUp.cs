using System;
using System.Threading;
using UnityEngine;
using Timer = System.Threading.Timer;

public class GrowUp : MonoBehaviour
{
    // ĳ���� ���� ��ũ��Ʈ

    private Timer timer;
    private SceneChange sceneChanger;   // �� ��ȯ ��ũ��Ʈ

    private const int EndAge = 10;
    private const int MaxMonth = 3;
    private const int TargetTime = 6;

    private void Start()
    {
        // �� ��ȯ ������Ʈ ����
        sceneChanger = GameManager.Instance.sceneChanger;

        UpdateCharacterMonth();
        UpdateNextMonthTimer();
    }

    // ĳ���� ���� �ð� ������Ʈ
    private void UpdateCharacterMonth()
    {
        DateTime lastGrowUpTime = GameManager.Instance.curCharacter.lastGrowUpTime;
        DateTime next6AM = lastGrowUpTime.Date.AddDays(1).AddHours(TargetTime);                  // ���� ���� �ð� : ������ ���� 6��

        if (DateTime.Now > next6AM)
        {
            // ���� �ð��� ���� �� ���� 6�ú��� �������� ĳ���� ����
            IncreaseMonth();
        }
    }

    // ���� ���� �ð� Ÿ�̸�
    private void UpdateNextMonthTimer()
    {
        DateTime currentTime = DateTime.Now;
        DateTime next6AM = currentTime.Date.AddHours(TargetTime);

        if (currentTime.Hour >= TargetTime)
        {
            next6AM = next6AM.AddDays(1);
        }

        TimeSpan timeUntilNext6AM = next6AM - currentTime;
        timer = new Timer(OnTimerElapsed, null, timeUntilNext6AM, Timeout.InfiniteTimeSpan);
    }

    // Ÿ�̸� �ð� ���
    private void OnTimerElapsed(object state)
    {
        IncreaseMonth();            // ĳ���� ����
        UpdateNextMonthTimer();     // ���� Ÿ�̸� �缳��
    }

    // ĳ���� ���� (ĳ������ month 1 ����)
    public void IncreaseMonth()
    {
        Character updateCharacter = GameManager.Instance.curCharacter;
        updateCharacter.month++;
        updateCharacter.UpdateLastGrowUpTime();      // ������ ���� �ð� ������Ʈ

        if (IsEndingAchieved(updateCharacter)) 
        {
            // ���� ������ �޼��ߴٸ� ���� ȹ��
            EndingAchieved(updateCharacter);
            return;
        }

        if (updateCharacter.month == MaxMonth)
        {
            // �ִ� �ð��� �����ٸ� ĳ���� ���� ����
            updateCharacter.age++;
            updateCharacter.month = 0;
            UpdateCharacterAppearance();
        }

        SaveCharacterData(updateCharacter);
    }

    // ���� ȹ�� ���� Ȯ��
    private bool IsEndingAchieved(Character character)
    {
        return character.age == EndAge && character.month == MaxMonth;
    }

    // ĳ���� ���� ����
    private void SaveCharacterData(Character character)
    {
        GameManager.Instance.curCharacter = character;
        GameManager.Instance.CharacterDatas[GameManager.Instance.CharacterDatas.Count - 1] = character;
        GameManager.Instance.jsonManager.SaveDataList(Constants.CharacterData, GameManager.Instance.CharacterDatas);
    }

    // ĳ���� ���� ������Ʈ
    private void UpdateCharacterAppearance()
    {
        GetComponent<CharacterAppearence>().UpdateCharacterAppearance();
    }

    // ���� ȹ��
    private void EndingAchieved(Character character)
    {
        character.endingAchieved = true;
        SaveCharacterData(character);
        Debug.Log("���� �޼�");
        sceneChanger.ChangeScene("Ending");
    }

    // Ÿ�̸� ����
    private void OnDestroy()
    {
        if (timer != null)
        {
            timer.Dispose();
        }
    }
}
