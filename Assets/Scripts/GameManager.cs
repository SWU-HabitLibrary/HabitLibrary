using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 게임 데이터를 관리하는 싱글톤 패턴
    public static GameManager instance;

    [Header("[Game Data]")]
    public Character playerCharacter;   // 플레이어 캐릭터 스텟


    public SceneChange sceneChanger;    // 씬 전환 스크립트
    public ScenarioObject scenarioObject;   // 시나리오 데이터
    public EndingObject endingObject;   // 엔딩 도감 데이터

    void Awake()
    {
        // 게임 시작과 동시에 싱글톤 구성

        if (instance)     //싱글톤 변수 instance가 이미 있다면
        {
            DestroyImmediate(gameObject);   //삭제
            return;
        }

        instance = this;    //유일한 인스턴스
        DontDestroyOnLoad(gameObject);  //씬이 바뀌어도 계속 유지시킴

        if (playerCharacter == null)
        {
            string characterFilePath = Application.dataPath + "/Saves/character.json";
            // 저장된 캐릭터 데이터가 있다면 가져옴
            if (File.Exists(characterFilePath))
                playerCharacter = this.GetComponent<CharacterStateJSON>().LoadToJson();
            else
                playerCharacter = new Character();
        }

        sceneChanger = this.GetComponent<SceneChange>();
        //UpdateGameDataFromSpreadSheet();
    }

    public static GameManager GetGameManager()
    {
        return instance;
    }

    public void UpdateGameDataFromSpreadSheet()
    {
        // 스프레드 시트로부터 게임 데이터를 불러오는 함수
        // 데이터 테이블에 변경사항 있을 때 딱 한 번 호출하기!
        // 비동기 방식으로 데이터를 불러오기 때문에, 데이터가 모두 불러와지면 저장 코드 실행

        int totalCount = 2; // 업데이트할 데이터의 총 개수
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
        endingObject.UpdateEndingData(onUpdateComplete);
    }
}
