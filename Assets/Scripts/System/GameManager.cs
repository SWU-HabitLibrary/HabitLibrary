using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 게임 데이터를 관리하는 싱글톤 패턴
    public static GameManager Instance { get; private set; }

    [Header("[Game Data]")]
    public JSONManager jsonManager; // 데이터 저장 매니저
    public SceneChange sceneChanger;    // 씬 전환 스크립트
    public ScenarioObject scenarioObject;   // 시나리오 데이터
    public CombatItemObject combatItemObject;   // 전투용 아이템 데이터
    public EndingObject endingObject;   // 엔딩 도감 데이터

    public List<Character> CharacterDatas { get; set; }
    public List<ItemData> CombatItemDatas { get; set; }
    public List<ItemData> EndingDatas { get; set; }

    [Header("[Player Data]")]
    public Character curCharacter;


    void Awake()
    {
        InitializeSingleton();
        InitializeGameData();
    }

    private void InitializeSingleton()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void InitializeGameData()
    {
        jsonManager = new JSONManager();
        sceneChanger = GetComponent<SceneChange>();
        //UpdateGameDataFromSpreadSheet();

        LoadOrCreatePlayerCharacter();
        LoadItemData();
    }

    private void LoadOrCreatePlayerCharacter()
    {
        if (jsonManager.CheckDataExists(Constants.CharacterData))
        {
            CharacterDatas = jsonManager.LoadDataList<Character>(Constants.CharacterData);
            curCharacter = CharacterDatas[CharacterDatas.Count - 1];
        }
        else
        {
            CharacterDatas = new List<Character>();
            curCharacter = new Character();
        }
    }

    private void LoadItemData()
    {
        CombatItemDatas = jsonManager.LoadDataList<ItemData>(Constants.CombatItemData);
        EndingDatas = jsonManager.LoadDataList<ItemData>(Constants.EndingData);
    }

    public void UpdateGameDataFromSpreadSheet()
    {
        // 스프레드 시트로부터 게임 데이터를 불러오는 함수
        // 데이터 테이블에 변경사항 있을 때 딱 한 번 호출하기!
        // 비동기 방식으로 데이터를 불러오기 때문에, 데이터가 모두 불러와지면 저장 코드 실행

        int totalCount = 3; // 업데이트할 데이터의 총 개수
        int updatedCount = 0;   // 업데이트된 데이터의 개수

        Action onUpdateComplete = () =>
        {
            updatedCount++;

            // 모든 데이터가 업데이트 되었을 때 저장
            if (updatedCount >= totalCount)
            {
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh(); // 변경사항 즉시 반영

                Debug.Log("데이터 테이블 저장 완료");
            }
        };

        scenarioObject.UpdateScenarioData(onUpdateComplete);
        combatItemObject.UpdateCombatItemData(onUpdateComplete);
        endingObject.UpdateEndingData(onUpdateComplete);
    }
}
