using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // ���� �̵��ϴ� ��ũ��Ʈ

    private void OnEnable()
    {
        RegisterSceneLoadedEvent();
    }

    private void OnDisable()
    {
        UnregisterSceneLoadedEvent();
    }

    private void RegisterSceneLoadedEvent()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void UnregisterSceneLoadedEvent()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �� ��ȯ ȿ��
        // �� ��ȯ ȿ����, ���̵� ȿ�� ������ ���� �ֱ�
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // �� �ε�
    }
}
