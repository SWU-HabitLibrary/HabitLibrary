using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    // ĳ���͸� �����ϴ� ��ũ��Ʈ

    private SceneChange sceneChanger;               // �� ��ȯ ��ũ��Ʈ
    public GameObject Panel_CreateCharacter;        // ĳ���� ���� �г�
    public InputField InputCharacterName;           // ĳ���� �̸� �Է�â
    public Text GenderText;                         // ĳ���� ���� ���� �ؽ�Ʈ
    public Text AgeText;                            // ĳ���� ���� ���� �ؽ�Ʈ

    private const int MinAge = 10;                  // ĳ���� ���� �ּҰ�
    private const int MaxAge = 19;                  // ĳ���� ���� �ִ�
    private int curAge = MinAge;                    // ���� ������ ĳ���� ���̰�
    private readonly string[] Genders = { "�ҳ�", "�ҳ�"};      // ���� ������ ĳ���� ������
    private int curGenderIndex = 0;                 // ���� ������ ĳ���� ���� �ε���

    private void Start()
    {
        // �� ��ȯ ��ũ��Ʈ ����
        sceneChanger = GameManager.Instance.sceneChanger;

        // ĳ���� ����, ���� �ؽ�Ʈ ������Ʈ
        UpdateGenderText();
        UpdateAgeText();
    }

    // ĳ���� ����
    public void CreateCharacter()
    {
        int id = GetNextCharacterId();              // ������ ĳ���� ���� ��ȣ
        string name = InputCharacterName.text;      // �Է��� ĳ���� �̸�
        string gender = Genders[curGenderIndex];       // ������ ĳ���� ����
        int age = curAge;
        Character newCharacter = new Character(id, name, gender, age);  // ĳ���� ����

        // ������ ����
        GameManager.Instance.curCharacter = newCharacter;
        GameManager.Instance.CharacterDatas.Add(newCharacter);
        GameManager.Instance.jsonManager.SaveDataList(Constants.CharacterData, GameManager.Instance.CharacterDatas);   

        // ���� ����
        SceneManager.LoadScene("Main");
    }

    // ������ ĳ���� ���� ��ȣ ��ȯ
    private int GetNextCharacterId()
    {
        // ������ ���ٸ� : ù��° ����
        if (!GameManager.Instance.jsonManager.CheckDataExists(Constants.CharacterData))
        {
            return 1;
        }

        // ������ ������ ����� ĳ���Ͱ� ���ٸ� : ù��° ����
        List<Character> characterList = GameManager.Instance.CharacterDatas;
        if (characterList.Count == 0)
        {
            return 1;
        }

        // ĳ���� ���� ��ȣ�� ������ Increment ������� ����
        int lastId = characterList[characterList.Count - 1].id;
        return lastId + 1;
    }

    // ���� ���� ���� ��ư (�ҳ� <-> �ҳ�)
    public void LeftButton_CharacterGender()
    {
        if (curGenderIndex == 0)
            curGenderIndex = 1;
        else
            curGenderIndex--;

        UpdateGenderText();
    }

    // ���� ���� ������ ��ư (�ҳ� <-> �ҳ�)
    public void RightButton_CharacterGender()
    {
        if (curGenderIndex == 1)
            curGenderIndex = 0;
        else
            curGenderIndex++;

        UpdateGenderText();
    }

    // ���� �ؽ�Ʈ ������Ʈ
    public void UpdateGenderText()
    {
        GenderText.text = Genders[curGenderIndex];
    }

    // ���� ���� ���� ��ư (�ּҳ��� -> �ִ볪��)
    public void LeftButton_CharacterAge()
    {
        curAge = (curAge <= MinAge) ? MaxAge : curAge - 1;
        UpdateAgeText();
    }

    // ���� ���� ������ ��ư (�ִ볪�� -> �ּҳ���)
    public void RightButton_CharacterAge()
    {
        curAge = (curAge >= MaxAge) ? MinAge : curAge + 1;
        UpdateAgeText();
    }

    // ���� �ؽ�Ʈ ������Ʈ
    public void UpdateAgeText()
    {
        AgeText.text = curAge.ToString();
    }

    // Ÿ��Ʋ ȭ�� ���� ���� ��ư
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
                // ������ ĳ���Ͱ� �ִٸ� �ε� �� ���� ����
                GameManager.Instance.curCharacter = characterList[characterList.Count - 1];
                sceneChanger.ChangeScene("Main");
            }
        }
    }

    // ĳ���� ���� �г� Ȱ��ȭ
    public void OpenCreateCharacterPanel()
    {
        Panel_CreateCharacter.SetActive(true);
    }

    // ĳ���� ���� �г� ��Ȱ��ȭ
    public void CloseCreateCharacterPanel()
    {
        Panel_CreateCharacter.SetActive(false);
    }
}