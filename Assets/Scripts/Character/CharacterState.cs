using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterState : MonoBehaviour
{
    // 메인 화면UI에서 캐릭터 스텟을 보여주는 스크립트

    public Text[] characterStates;

    void Start()
    {
        UpdateCharacterStates();
    }

    // Update is called once per frame
    public void UpdateCharacterStates()
    {
        Character playerCharacter = GameManager.instance.playerCharacter;
        characterStates[0].text = playerCharacter.name;
        characterStates[1].text = "체력 : " + playerCharacter.hp;
        characterStates[2].text = "마력 : " + playerCharacter.mp;
        characterStates[3].text = "정신력 : " + playerCharacter.stress;
        characterStates[4].text = "지성 : " + playerCharacter.intelligence;
        characterStates[5].text = playerCharacter.gold + "G";
    }
}
