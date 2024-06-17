using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingStars : MonoBehaviour
{
    // Ÿ��Ʋ ȭ�� �ִϸ��̼� ��ũ��Ʈ

    public GameObject[] stars; // ���˺� �迭
    private Vector2[] initialPositions; // ���˺� �ʱ� ��ġ ���� �迭

    private float resetX = -1100f; // ������ϴ� X ��ǥ ����(ȭ�� ������ ����� X ��ǥ ����)
    private float resetY = -1440f; // ������ϴ� Y ��ǥ ����(ȭ�� ������ ����� Y ��ǥ ����)
    private float speed = -4f;  // �������� �ӵ�

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

        StartCoroutine(ActivateAndMoveStars());
    }

    private IEnumerator ActivateAndMoveStars()
    {
        while (true)
        {
            foreach (GameObject star in stars)
            {
                if (!star.activeInHierarchy)        // Ȱ��ȭ �Ǿ����� ������
                {
                    star.SetActive(true);           // ���˺� Ȱ��ȭ
                    StartCoroutine(MoveStar(star));    // ���˺� ��ǥ �̵�
                    yield return new WaitForSeconds(0.1f); 
                }
            }

            yield return null;
        }
    }

    private IEnumerator MoveStar(GameObject star)
    {
        RectTransform rectTransform = star.GetComponent<RectTransform>();
        int index = System.Array.IndexOf(stars, star);

        while (true)
        {
            rectTransform.anchoredPosition += new Vector2(speed, speed);

            if (rectTransform.anchoredPosition.x <= resetX || rectTransform.anchoredPosition.y <= resetY)
            {
                ResetStarPosition(rectTransform, index);
                break;
            }

            yield return null; // �� �����Ӹ��� �̵�
        }
    }

    private void ResetStarPosition(RectTransform rectTransform, int index)
    {
        rectTransform.anchoredPosition = initialPositions[index];
        stars[index].SetActive(false);
    }
}
