using System;
using System.Threading;
using UnityEngine;
using Timer = System.Threading.Timer;

public class GrowUp : MonoBehaviour
{
    // 캐릭터 성장 스크립트

    private Timer timer;
    private SceneChange sceneChanger;   // 씬 전환 스크립트

    private const int EndAge = 10;
    private const int MaxMonth = 3;
    private const int TargetTime = 6;

    private void Start()
    {
        // 씬 전환 컴포넌트 연결
        sceneChanger = GameManager.Instance.sceneChanger;

        UpdateCharacterMonth();
        UpdateNextMonthTimer();
    }

    // 캐릭터 성장 시간 업데이트
    private void UpdateCharacterMonth()
    {
        DateTime lastGrowUpTime = GameManager.Instance.curCharacter.lastGrowUpTime;
        DateTime next6AM = lastGrowUpTime.Date.AddDays(1).AddHours(TargetTime);                  // 성장 기준 시간 : 다음날 오전 6시

        if (DateTime.Now > next6AM)
        {
            // 현재 시간이 다음 날 오전 6시보다 지났으면 캐릭터 성장
            IncreaseMonth();
        }
    }

    // 다음 성장 시간 타이머
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

    // 타이머 시간 경과
    private void OnTimerElapsed(object state)
    {
        IncreaseMonth();            // 캐릭터 성장
        UpdateNextMonthTimer();     // 다음 타이머 재설정
    }

    // 캐릭터 성장 (캐릭터의 month 1 증가)
    public void IncreaseMonth()
    {
        Character updateCharacter = GameManager.Instance.curCharacter;
        updateCharacter.month++;
        updateCharacter.UpdateLastGrowUpTime();      // 마지막 성장 시간 업데이트

        if (IsEndingAchieved(updateCharacter)) 
        {
            // 엔딩 조건을 달성했다면 엔딩 획득
            EndingAchieved(updateCharacter);
            return;
        }

        if (updateCharacter.month == MaxMonth)
        {
            // 최대 시간이 지났다면 캐릭터 나이 증가
            updateCharacter.age++;
            updateCharacter.month = 0;
            UpdateCharacterAppearance();
        }

        SaveCharacterData(updateCharacter);
    }

    // 엔딩 획득 조건 확인
    private bool IsEndingAchieved(Character character)
    {
        return character.age == EndAge && character.month == MaxMonth;
    }

    // 캐릭터 정보 저장
    private void SaveCharacterData(Character character)
    {
        GameManager.Instance.curCharacter = character;
        GameManager.Instance.CharacterDatas[GameManager.Instance.CharacterDatas.Count - 1] = character;
        GameManager.Instance.jsonManager.SaveDataList(Constants.CharacterData, GameManager.Instance.CharacterDatas);
    }

    // 캐릭터 외형 업데이트
    private void UpdateCharacterAppearance()
    {
        GetComponent<CharacterAppearence>().UpdateCharacterAppearance();
    }

    // 엔딩 획득
    private void EndingAchieved(Character character)
    {
        character.endingAchieved = true;
        SaveCharacterData(character);
        Debug.Log("엔딩 달성");
        sceneChanger.ChangeScene("Ending");
    }

    // 타이머 해제
    private void OnDestroy()
    {
        if (timer != null)
        {
            timer.Dispose();
        }
    }
}
