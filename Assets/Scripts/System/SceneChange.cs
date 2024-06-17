using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // 씬을 이동하는 스크립트

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
        // 씬 전환 효과
        // 씬 전환 효과음, 페이드 효과 있으면 여기 넣기
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // 씬 로드
    }
}
