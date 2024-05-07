using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingBook : MonoBehaviour
{
    public GameObject Panel_CollectBook;    // ���� �г�
    public GameObject endingContents;
    public GameObject Item_Ending;  // ���� ������ ������
    public List<Ending> endings;    // ���� ������

    private int curIndex = 0;

    public void Start()
    {
        endings = GameManager.instance.endingObject.dataList;
    }

    public void ShowEndingItem()
    {
        // ���� ���� ������ŭ �����ϴ� �Լ�

        // ���� �ε������� ���� 4���� �������� ǥ��
        for (int i = 0; i < 4; i++)
        {
            for (int j = curIndex; j < endings.Count; j++)
            {
                GameObject newItem = Instantiate(Item_Ending);
            }
        }

    }

    public void OpenCollectBook()
    {
        Panel_CollectBook.SetActive(true);
    }

    public void CloseCollectBook()
    {
        Panel_CollectBook.SetActive(false);
    }

}
