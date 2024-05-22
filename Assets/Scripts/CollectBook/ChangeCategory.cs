using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCategory : MonoBehaviour
{
    public GameObject Panel_CollectBook;    // ���� �г�
    public GameObject[] CollectBookCategories;   // ���� ��������

    public void OpenCollectBook()
    {
        Panel_CollectBook.SetActive(true);
    }

    public void CloseCollectBook()
    {
        Panel_CollectBook.SetActive(false);
    }

    public void OpenInventoryBook()
    {
        for (int i = 0; i < CollectBookCategories.Length; i++)
        {
            if (i == 0)
                continue;
            else
                CollectBookCategories[i].SetActive(false);
        }

        CollectBookCategories[0].SetActive(true);
        GameObject.FindWithTag("CollectBookManager").GetComponent<CombatItemBook>().ShowCombatItem();
    }

    public void OpenEndingBook()
    {
        for (int i = 0; i < CollectBookCategories.Length; i++)
        {
            if (i == 1)
                continue;
            else
                CollectBookCategories[i].SetActive(false);
        }

        CollectBookCategories[1].SetActive(true);
    }
}
