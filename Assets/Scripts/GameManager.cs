using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ���� �����͸� �����ϴ� �̱��� ����
    public static GameManager instance;

    [Header("[Game Data]")]
    public Character playerCharacter;   // �÷��̾� ĳ���� ����

    void Awake()
    {
        // ���� ���۰� ���ÿ� �̱��� ����

        if (instance)     //�̱��� ���� instance�� �̹� �ִٸ�
        {
            DestroyImmediate(gameObject);   //����
            return;
        }

        instance = this;    //������ �ν��Ͻ�
        DontDestroyOnLoad(gameObject);  //���� �ٲ� ��� ������Ŵ

        if (playerCharacter == null)
        {
            string characterFilePath = Application.dataPath + "/Saves/character.json";
            // ����� ĳ���� �����Ͱ� �ִٸ� ������
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
