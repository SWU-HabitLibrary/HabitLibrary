using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    // 캐릭터를 생성하는 스크립트

    private SceneChange sceneChanger;               // 씬 전환 스크립트
    public GameObject Panel_CreateCharacter;        // 캐릭터 생성 패널
    public InputField InputCharacterName;           // 캐릭터 이름 입력창
    public Text GenderText;                         // 캐릭터 성별 선택 텍스트
    public Text AgeText;                            // 캐릭터 나이 선택 텍스트

    private const int MinAge = 10;                  // 캐릭터 나이 최소값
    private const int MaxAge = 19;                  // 캐릭터 나이 최댓값
    private int curAge = MinAge;                    // 현재 선택한 캐릭터 나이값
    private readonly string[] Genders = { "소녀", "소년"};      // 선택 가능한 캐릭터 성별값
    private int curGenderIndex = 0;                 // 현재 선택한 캐릭터 성별 인덱스

    private void Start()
    {
        // 씬 전환 스크립트 연결
        sceneChanger = GameManager.Instance.sceneChanger;

        // 캐릭터 성별, 나이 텍스트 업데이트
        UpdateGenderText();
        UpdateAgeText();
    }

    // 캐릭터 생성
    public void CreateCharacter()
    {
        int id = GetNextCharacterId();              // 생성할 캐릭터 고유 번호
        string name = InputCharacterName.text;      // 입력한 캐릭터 이름
        string gender = Genders[curGenderIndex];       // 선택한 캐릭터 성별
        int age = curAge;
        Character newCharacter = new Character(id, name, gender, age);  // 캐릭터 생성

        // 데이터 저장
        GameManager.Instance.curCharacter = newCharacter;
        GameManager.Instance.CharacterDatas.Add(newCharacter);
        GameManager.Instance.jsonManager.SaveDataList(Constants.CharacterData, GameManager.Instance.CharacterDatas);   

        // 게임 시작
        SceneManager.LoadScene("Main");
    }

    // 생성할 캐릭터 고유 번호 반환
    private int GetNextCharacterId()
    {
        // 파일이 없다면 : 첫번째 생성
        if (!GameManager.Instance.jsonManager.CheckDataExists(Constants.CharacterData))
        {
            return 1;
        }

        // 파일은 있으나 저장된 캐릭터가 없다면 : 첫번째 생성
        List<Character> characterList = GameManager.Instance.CharacterDatas;
        if (characterList.Count == 0)
        {
            return 1;
        }

        // 캐릭터 고유 번호를 간단한 Increment 방식으로 생성
        int lastId = characterList[characterList.Count - 1].id;
        return lastId + 1;
    }

    // 성별 선택 왼쪽 버튼 (소녀 <-> 소년)
    public void LeftButton_CharacterGender()
    {
        if (curGenderIndex == 0)
            curGenderIndex = 1;
        else
            curGenderIndex--;

        UpdateGenderText();
    }

    // 성별 선택 오른쪽 버튼 (소녀 <-> 소년)
    public void RightButton_CharacterGender()
    {
        if (curGenderIndex == 1)
            curGenderIndex = 0;
        else
            curGenderIndex++;

        UpdateGenderText();
    }

    // 성별 텍스트 업데이트
    public void UpdateGenderText()
    {
        GenderText.text = Genders[curGenderIndex];
    }

    // 나이 선택 왼쪽 버튼 (최소나이 -> 최대나이)
    public void LeftButton_CharacterAge()
    {
        curAge = (curAge <= MinAge) ? MaxAge : curAge - 1;
        UpdateAgeText();
    }

    // 나이 선택 오른쪽 버튼 (최대나이 -> 최소나이)
    public void RightButton_CharacterAge()
    {
        curAge = (curAge >= MaxAge) ? MinAge : curAge + 1;
        UpdateAgeText();
    }

    // 나이 텍스트 업데이트
    public void UpdateAgeText()
    {
        AgeText.text = curAge.ToString();
    }

    // 타이틀 화면 게임 시작 버튼
    public void StartButton()
    {
        if (!GameManager.Instance.jsonManager.CheckDataExists(Constants.CharacterData))
        {
            OpenCreateCharacterPanel();
        }
        else  
        {
            List<Character> characterList = GameManager.Instance.CharacterDatas;
            if (characterList.Count > 0 && characterList[characterList.Count - 1].endingAchieved)
            {
                OpenCreateCharacterPanel();
            }
            else
            {
                // 생성된 캐릭터가 있다면 로드 및 게임 시작
                GameManager.Instance.curCharacter = characterList[characterList.Count - 1];
                sceneChanger.ChangeScene("Main");
            }
        }
    }

    // 캐릭터 생성 패널 활성화
    public void OpenCreateCharacterPanel()
    {
        Panel_CreateCharacter.SetActive(true);
    }

    // 캐릭터 생성 패널 비활성화
    public void CloseCreateCharacterPanel()
    {
        Panel_CreateCharacter.SetActive(false);
    }
}