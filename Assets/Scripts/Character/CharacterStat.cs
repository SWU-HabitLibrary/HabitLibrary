using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStat : MonoBehaviour
{
    // 캐릭터 스텟 UI 스크립트

    public Text[] characterStates;

    private const int HpIndex = 0;
    private const int MpIndex = 1;
    private const int IntelligenceIndex = 2;
    private const int GoldIndex = 3;

    void Start()
    {
        UpdateCharacterStats();
    }

    // Update is called once per frame
    public void UpdateCharacterStats()
    {
        Character playerCharacter = GameManager.Instance.curCharacter;

        characterStates[HpIndex].text = playerCharacter.hp.ToString();
        characterStates[MpIndex].text = playerCharacter.mp.ToString();
        characterStates[IntelligenceIndex].text = playerCharacter.intelligence.ToString();
        characterStates[GoldIndex].text = playerCharacter.gold.ToString() + " G";
    }
}
