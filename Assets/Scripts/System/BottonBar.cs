using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottonBar : MonoBehaviour
{
    public GameObject SchedulePanel;    // 스케줄 패널
    private SceneChange sceneChanger;   // 씬 전환 스크립트

    public void Start()
    {
        // 한 번만 호출
        sceneChanger = GameManager.instance.sceneChanger;
    }

    public void MoveExplorationScene()
    {
        // 탐험 씬으로 이동하는 버튼
        sceneChanger.ChangeScene("Exploration");
    }

    public void OpenSchedulePanel()
    {
        // 스케줄 패널을 여는 함수

        SchedulePanel.SetActive(true);
    }

    public void CloseSchedulePanel()
    {
        // 스케줄 패널을 닫는 함수

        SchedulePanel.SetActive(false);
    }
}
