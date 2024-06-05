using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingStars : MonoBehaviour
{
    // 시작 화면에서 별이 떨어지는 애니메이션

    public GameObject[] stars; // 별똥별 배열
    private Vector2[] initialPositions; // 별똥별 초기 위치 저장 배열

    private float resetX = -1100f; // 재생성하는 X 좌표 기준(화면 밖으로 벗어나는 X 좌표 기준)
    private float resetY = -1440f; // 재생성하는 Y 좌표 기준(화면 밖으로 벗어나는 Y 좌표 기준)
    private float speed = -4f;  // 떨어지는 속도

    private void Start()
    {
        InitializeImages();
    }

    private void InitializeImages()
    {
        initialPositions = new Vector2[stars.Length];

        for (int i = 0; i < stars.Length; i++)
        {
            RectTransform rectTransform = stars[i].GetComponent<RectTransform>();
            initialPositions[i] = rectTransform.anchoredPosition;
            stars[i].SetActive(false);
        }

        StartCoroutine(ActivateAndMoveImages());
    }

    private IEnumerator ActivateAndMoveImages()
    {
        while (true)
        {
            foreach (GameObject star in stars)
            {
                if (!star.activeInHierarchy)        // 활성화 되어있지 않으면
                {
                    star.SetActive(true);           // 별똥별 활성화
                    StartCoroutine(MoveImage(star));    // 별똥별 좌표 이동
                    yield return new WaitForSeconds(0.1f); 
                }
            }

            yield return null;
        }
    }

    private IEnumerator MoveImage(GameObject star)
    {
        RectTransform rectTransform = star.GetComponent<RectTransform>();
        int index = System.Array.IndexOf(stars, star);

        while (true)
        {
            rectTransform.anchoredPosition += new Vector2(speed, speed);

            if (rectTransform.anchoredPosition.x <= resetX || rectTransform.anchoredPosition.y <= resetY)
            {
                rectTransform.anchoredPosition = initialPositions[index];
                star.SetActive(false);
                break;
            }

            yield return null; // 매 프레임마다 이동
        }
    }
}
