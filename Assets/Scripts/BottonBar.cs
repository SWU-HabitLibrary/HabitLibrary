using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BottonBar : MonoBehaviour
{
    public GameObject SchedulePanel;    // ������ �г�

    public void MoveExplorationScene()
    {
        // Ž�� ������ �̵��ϴ� ��ư

        SceneManager.LoadScene("Exploration");
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
