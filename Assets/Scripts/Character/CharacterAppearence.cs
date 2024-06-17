using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterAppearence : MonoBehaviour
{
    // ĳ���� ���� ���� ��ũ��Ʈ

    public SpriteRenderer characterSpriteRenderer;   // ĳ���� ��������Ʈ ������

    public Sprite girlLittleSprite;                  // �ҳ� Little �̹���
    public Sprite girlMiddleSprite;                  // �ҳ� Middle �̹���
    public Sprite girlBigSprite;                     // �ҳ� Big �̹���

    public Sprite boyLittleSprite;                   // �ҳ� Little �̹���
    public Sprite boyMiddleSprite;                   // �ҳ� Middle �̹���
    public Sprite boyBigSprite;                      // �ҳ� Big �̹���

    private const int Little = 0;
    private const int Middle = 1;
    private const int Big = 2;

    private void Start()
    {
        // ĳ���� ���� ������Ʈ
        UpdateCharacterAppearance();
    }

    // ĳ���� ���� ������Ʈ
    public void UpdateCharacterAppearance()
    {
        Character curCharacter = GameManager.Instance.curCharacter;

        string gender = curCharacter.gender;
        int age = curCharacter.age;

        SetCharacterAppearance(gender, age);
    }

    // ���� ���� �޼���
    private void SetCharacterAppearance(string gender, int age)
    {
        int ageIndex = GetAgeStageIndex(age);
        Sprite newSprite = null;

        switch (gender)
        {
            case "�ҳ�":
                newSprite = GetGirlAppearance(ageIndex);
                break;
            case "�ҳ�":
                newSprite = GetBoyAppearance(ageIndex);
                break;
            default:
                Debug.LogError("�߸��� �����Դϴ�.");
                break;
        }

        characterSpriteRenderer.sprite = newSprite;
    }

    // �ҳ� ���� ����
    private Sprite GetGirlAppearance(int ageIndex)
    {
        switch (ageIndex)
        {
            case Little: 
                return girlLittleSprite;
            case Middle: 
                return girlMiddleSprite;
            case Big: 
                return girlBigSprite;
            default:
                Debug.LogError("�߸��� ���� �ε����Դϴ�.");
                return null;
        }
    }


    // �ҳ� ���� ����
    private Sprite GetBoyAppearance(int ageIndex)
    {
        switch (ageIndex)
        {
            case Little:
                return boyLittleSprite;
            case Middle:
                return boyMiddleSprite;
            case Big:  
                return boyBigSprite;
            default:
                Debug.LogError("�߸��� ���� �ε����Դϴ�.");
                return null;
        }
    }


    // ���̿� ���� ���� ���� �ε��� ��ȯ
    private int GetAgeStageIndex(int age)
    {
        if (age >= 10 && age <= 13)
            return Little; 
        else if (age >= 14 && age <= 16)
            return Middle; 
        else if (age >= 17 && age <= 20)
            return Big;  
        else
        {
            Debug.LogError("�߸��� �����Դϴ�.");
            return Little;  // �⺻���� Little ����
        }
    }
}
