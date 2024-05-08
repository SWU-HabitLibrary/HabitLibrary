using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottonBar : MonoBehaviour
{
    public GameObject SchedulePanel;    // ������ �г�
    private SceneChange sceneChanger;   // �� ��ȯ ��ũ��Ʈ

    public void Start()
    {
        // �� ���� ȣ��
        sceneChanger = GameManager.instance.sceneChanger;
    }

    public void MoveExplorationScene()
    {
        // Ž�� ������ �̵��ϴ� ��ư
        sceneChanger.ChangeScene("Exploration");
    }

    public void OpenSchedulePanel()
    {
        // ������ �г��� ���� �Լ�

        SchedulePanel.SetActive(true);
    }

    public void CloseSchedulePanel()
    {
        // ������ �г��� �ݴ� �Լ�

        SchedulePanel.SetActive(false);
    }
}
