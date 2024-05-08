using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ���� �����͸� �����ϴ� �̱��� ����
    public static GameManager instance;

    [Header("[Game Data]")]
    public Character playerCharacter;   // �÷��̾� ĳ���� ����
    public List<ItemData> combatItemDatas;  // �÷��̾� ������ ������ ������
    public List<ItemData> endingDatas;  // �÷��̾� ���� ������


    public SceneChange sceneChanger;    // �� ��ȯ ��ũ��Ʈ
    public ScenarioObject scenarioObject;   // �ó����� ������
    public CombatItemObject combatItemObject;   // ������ ������ ������
    public EndingObject endingObject;   // ���� ���� ������

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

        sceneChanger = this.GetComponent<SceneChange>();
        //UpdateGameDataFromSpreadSheet();

        combatItemDatas = this.GetComponent<ItemDataJSON>().UpdateItemData("CombatItemData");
        endingDatas = this.GetComponent<ItemDataJSON>().UpdateItemData("EndingData");

    }

    public static GameManager GetGameManager()
    {
        return instance;
    }

    public void UpdateGameDataFromSpreadSheet()
    {
        // �������� ��Ʈ�κ��� ���� �����͸� �ҷ����� �Լ�
        // ������ ���̺��� ������� ���� �� �� �� �� ȣ���ϱ�!
        // �񵿱� ������� �����͸� �ҷ����� ������, �����Ͱ� ��� �ҷ������� ���� �ڵ� ����

        int totalCount = 3; // ������Ʈ�� �������� �� ����
        int updatedCount = 0;   // ������Ʈ�� �������� ����

        Action onUpdateComplete = () =>
        {
            updatedCount++;

            // ��� �����Ͱ� ������Ʈ �Ǿ��� �� ����
            if (updatedCount >= totalCount)
            {
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh(); // ������� ��� �ݿ�

                Debug.Log("������ ���̺� ���� �Ϸ�");
            }
        };

        scenarioObject.UpdateScenarioData(onUpdateComplete);
        combatItemObject.UpdateCombatItemData(onUpdateComplete);
        endingObject.UpdateEndingData(onUpdateComplete);
    }
}