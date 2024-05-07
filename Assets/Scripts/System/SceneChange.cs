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
        // ��������Ʈ ü�� �߰�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �� ��ȯ ȿ��
        // �� ��ȯ ȿ����, ���̵� ȿ�� ������ ���� �ֱ�
        
    }

    private void OnDisable()
    {
        // ��������Ʈ ü�� ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);  // �� �ε�
    }
}
