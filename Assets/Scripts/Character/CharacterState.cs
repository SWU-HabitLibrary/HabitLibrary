using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterState : MonoBehaviour
{
    // ���� ȭ��UI���� ĳ���� ������ �����ִ� ��ũ��Ʈ

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
        characterStates[1].text = "ü�� : " + playerCharacter.hp;
        characterStates[2].text = "���� : " + playerCharacter.mp;
        characterStates[3].text = "���ŷ� : " + playerCharacter.stress;
        characterStates[4].text = "���� : " + playerCharacter.intelligence;
        characterStates[5].text = playerCharacter.gold + "G";
    }
}
