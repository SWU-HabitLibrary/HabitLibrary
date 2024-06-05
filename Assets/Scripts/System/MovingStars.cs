using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingStars : MonoBehaviour
{
    // ���� ȭ�鿡�� ���� �������� �ִϸ��̼�

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

        StartCoroutine(ActivateAndMoveImages());
    }

    private IEnumerator ActivateAndMoveImages()
    {
        while (true)
        {
            foreach (GameObject star in stars)
            {
                if (!star.activeInHierarchy)        // Ȱ��ȭ �Ǿ����� ������
                {
                    star.SetActive(true);           // ���˺� Ȱ��ȭ
                    StartCoroutine(MoveImage(star));    // ���˺� ��ǥ �̵�
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

            yield return null; // �� �����Ӹ��� �̵�
        }
    }
}
