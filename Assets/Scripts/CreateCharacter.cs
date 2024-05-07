using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    // ĳ���͸� �����ϴ� ��ũ��Ʈ(���߿� ��ɺ��� ��ũ��Ʈ �и�)

    public GameObject Panel_CreateCharacter;    // ĳ���� ���� �г�
    public InputField InputCharacterName;   // ĳ���� �̸� �Է�â
    public GameObject[] CharacterSex;   // ���� ������ ĳ���� �迭

    private int curCharacterSex = 0;    // ���� ���ð�
    private SceneChange sceneChanger;   // �� ��ȯ ��ũ��Ʈ

    private void Start()
    {
        sceneChanger = GameManager.instance.sceneChanger;
        ViewCharacterSelect(curCharacterSex);
    }

    public void CharacterSelect()
    {
        // ĳ���� ���� ��ư�� Ŭ��

        string name = InputCharacterName.text;  // �Է��� ĳ���� �̸�
        string sex = CharacterSex[curCharacterSex].GetComponent<Text>().text;   // ������ ĳ���� ����
        Character newCharacter = new Character(name, sex);  // ĳ���� ����

        GameManager.instance.playerCharacter = newCharacter; 
        GameManager.instance.GetComponent<CharacterStateJSON>().SaveToJson(newCharacter);   // ** �� �κ� JSON Manager ���� ����

        SceneManager.LoadScene("Main");
    }

    public void LeftButton_CharacterSelect()
    {
        // ĳ���� ����â���� ���� ��ư�� Ŭ��
        if (curCharacterSex < 1)
            curCharacterSex++;
        else
            curCharacterSex--;

        // ĳ���� ���� �̹��� ����(�ӽ÷� �ؽ�Ʈ ����)
        ViewCharacterSelect(curCharacterSex);
    }

    public void RightButton_CharacterSelect()
    {
        // ĳ���� ����â���� ������ ��ư�� Ŭ��
        if (curCharacterSex > 0)
            curCharacterSex--;
        else
            curCharacterSex++;
        // ĳ���� ���� �̹��� ����(�ӽ÷� �ؽ�Ʈ ����)
        ViewCharacterSelect(curCharacterSex);
    }

    public void ViewCharacterSelect(int index)
    {
        // �̹��� ����
        for (int i = 0; i < CharacterSex.Length; i++)
        {
            CharacterSex[i].SetActive(false);
        }

        CharacterSex[index].SetActive(true);
    }

    public void StartButton()
    {
        // ���� ���� ��ư

        string characterFilePath = Application.dataPath + "/Saves/character.json";
        // ����� ĳ���� �����Ͱ� ���ٸ� ĳ���� ����â ����
        if (!File.Exists(characterFilePath))
            OpenCreateCharacterPanel();
        else    // ����� ĳ���� �����Ͱ� �ִٸ� �� �̵�
            sceneChanger.ChangeScene("Main");
    }

    public void OpenCreateCharacterPanel()
    {
        // ĳ���� ���� �г��� ��

        Panel_CreateCharacter.SetActive(true);
    }

    public void CloseCreateCharacterPanel()
    {
        // ĳ���� ���� �г��� ����
        Panel_CreateCharacter.SetActive(false);
    }
}
