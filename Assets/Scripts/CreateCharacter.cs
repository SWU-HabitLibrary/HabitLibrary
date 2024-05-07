using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    // 캐릭터를 생성하는 스크립트(나중에 기능별로 스크립트 분리)

    public GameObject Panel_CreateCharacter;    // 캐릭터 생성 패널
    public InputField InputCharacterName;   // 캐릭터 이름 입력창
    public GameObject[] CharacterSex;   // 선택 가능한 캐릭터 배열

    private int curCharacterSex = 0;    // 현재 선택값
    private SceneChange sceneChanger;   // 씬 전환 스크립트

    private void Start()
    {
        sceneChanger = GameManager.instance.sceneChanger;
        ViewCharacterSelect(curCharacterSex);
    }

    public void CharacterSelect()
    {
        // 캐릭터 선택 버튼을 클릭

        string name = InputCharacterName.text;  // 입력한 캐릭터 이름
        string sex = CharacterSex[curCharacterSex].GetComponent<Text>().text;   // 선택한 캐릭터 성별
        Character newCharacter = new Character(name, sex);  // 캐릭터 생성

        GameManager.instance.playerCharacter = newCharacter; 
        GameManager.instance.GetComponent<CharacterStateJSON>().SaveToJson(newCharacter);   // ** 이 부분 JSON Manager 만들어서 변경

        SceneManager.LoadScene("Main");
    }

    public void LeftButton_CharacterSelect()
    {
        // 캐릭터 선택창에서 왼쪽 버튼을 클릭
        if (curCharacterSex < 1)
            curCharacterSex++;
        else
            curCharacterSex--;

        // 캐릭터 성별 이미지 변경(임시로 텍스트 변경)
        ViewCharacterSelect(curCharacterSex);
    }

    public void RightButton_CharacterSelect()
    {
        // 캐릭터 선택창에서 오른쪽 버튼을 클릭
        if (curCharacterSex > 0)
            curCharacterSex--;
        else
            curCharacterSex++;
        // 캐릭터 성별 이미지 변경(임시로 텍스트 변경)
        ViewCharacterSelect(curCharacterSex);
    }

    public void ViewCharacterSelect(int index)
    {
        // 이미지 리셋
        for (int i = 0; i < CharacterSex.Length; i++)
        {
            CharacterSex[i].SetActive(false);
        }

        CharacterSex[index].SetActive(true);
    }

    public void StartButton()
    {
        // 게임 시작 버튼

        string characterFilePath = Application.dataPath + "/Saves/character.json";
        // 저장된 캐릭터 데이터가 없다면 캐릭터 생성창 열기
        if (!File.Exists(characterFilePath))
            OpenCreateCharacterPanel();
        else    // 저장된 캐릭터 데이터가 있다면 씬 이동
            sceneChanger.ChangeScene("Main");
    }

    public void OpenCreateCharacterPanel()
    {
        // 캐릭터 생성 패널을 엶

        Panel_CreateCharacter.SetActive(true);
    }

    public void CloseCreateCharacterPanel()
    {
        // 캐릭터 생성 패널을 닫음
        Panel_CreateCharacter.SetActive(false);
    }
}
