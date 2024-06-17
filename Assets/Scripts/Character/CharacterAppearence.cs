using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterAppearence : MonoBehaviour
{
    // 캐릭터 외형 변경 스크립트

    public SpriteRenderer characterSpriteRenderer;   // 캐릭터 스프라이트 렌더러

    public Sprite girlLittleSprite;                  // 소녀 Little 이미지
    public Sprite girlMiddleSprite;                  // 소녀 Middle 이미지
    public Sprite girlBigSprite;                     // 소녀 Big 이미지

    public Sprite boyLittleSprite;                   // 소년 Little 이미지
    public Sprite boyMiddleSprite;                   // 소년 Middle 이미지
    public Sprite boyBigSprite;                      // 소년 Big 이미지

    private const int Little = 0;
    private const int Middle = 1;
    private const int Big = 2;

    private void Start()
    {
        // 캐릭터 외형 업데이트
        UpdateCharacterAppearance();
    }

    // 캐릭터 외형 업데이트
    public void UpdateCharacterAppearance()
    {
        Character curCharacter = GameManager.Instance.curCharacter;

        string gender = curCharacter.gender;
        int age = curCharacter.age;

        SetCharacterAppearance(gender, age);
    }

    // 외형 설정 메서드
    private void SetCharacterAppearance(string gender, int age)
    {
        int ageIndex = GetAgeStageIndex(age);
        Sprite newSprite = null;

        switch (gender)
        {
            case "소녀":
                newSprite = GetGirlAppearance(ageIndex);
                break;
            case "소년":
                newSprite = GetBoyAppearance(ageIndex);
                break;
            default:
                Debug.LogError("잘못된 성별입니다.");
                break;
        }

        characterSpriteRenderer.sprite = newSprite;
    }

    // 소녀 외형 설정
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
                Debug.LogError("잘못된 나이 인덱스입니다.");
                return null;
        }
    }


    // 소년 외형 설정
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
                Debug.LogError("잘못된 나이 인덱스입니다.");
                return null;
        }
    }


    // 나이에 따라 나이 범위 인덱스 반환
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
            Debug.LogError("잘못된 나이입니다.");
            return Little;  // 기본으로 Little 설정
        }
    }
}
