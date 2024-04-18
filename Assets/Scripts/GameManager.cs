using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 게임 데이터를 관리하는 싱글톤 패턴
    public static GameManager instance;

    [Header("[Game Data]")]
    public Character playerCharacter;   // 플레이어 캐릭터 스텟

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
    }

    public static GameManager GetGameManager()
    {
        return instance;
    }

}
