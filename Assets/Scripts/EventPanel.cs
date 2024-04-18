using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventPanel : MonoBehaviour
{
    public GameObject eventPanel;

    public void OpanEventPanel()
    {
        eventPanel.SetActive(true);
    }

    public void CloseEventPanel()
    {
        eventPanel.SetActive(false);
    }

    public void MoveMainScene()
    {
        SceneManager.LoadScene("Main");
    }
}
