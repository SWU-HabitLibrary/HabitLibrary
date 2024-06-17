using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ���� �����͸� �����ϴ� �̱��� ����
    public static GameManager Instance { get; private set; }

    [Header("[Game Data]")]
    public JSONManager jsonManager; // ������ ���� �Ŵ���
    public SceneChange sceneChanger;    // �� ��ȯ ��ũ��Ʈ
    public ScenarioObject scenarioObject;   // �ó����� ������
    public CombatItemObject combatItemObject;   // ������ ������ ������
    public EndingObject endingObject;   // ���� ���� ������

    public List<Character> CharacterDatas { get; set; }
    public List<ItemData> CombatItemDatas { get; set; }
    public List<ItemData> EndingDatas { get; set; }

    [Header("[Player Data]")]
    public Character curCharacter;


    void Awake()
    {
        InitializeSingleton();
        InitializeGameData();
    }

    private void InitializeSingleton()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void InitializeGameData()
    {
        jsonManager = new JSONManager();
        sceneChanger = GetComponent<SceneChange>();
        //UpdateGameDataFromSpreadSheet();

        LoadOrCreatePlayerCharacter();
        LoadItemData();
    }

    private void LoadOrCreatePlayerCharacter()
    {
        if (jsonManager.CheckDataExists(Constants.CharacterData))
        {
            CharacterDatas = jsonManager.LoadDataList<Character>(Constants.CharacterData);
            curCharacter = CharacterDatas[CharacterDatas.Count - 1];
        }
        else
        {
            CharacterDatas = new List<Character>();
            curCharacter = new Character();
        }
    }

    private void LoadItemData()
    {
        CombatItemDatas = jsonManager.LoadDataList<ItemData>(Constants.CombatItemData);
        EndingDatas = jsonManager.LoadDataList<ItemData>(Constants.EndingData);
    }

    public void UpdateGameDataFromSpreadSheet()
    {
        // �������� ��Ʈ�κ��� ���� �����͸� �ҷ����� �Լ�
        // ������ ���̺� ������� ���� �� �� �� �� ȣ���ϱ�!
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
