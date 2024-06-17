using UnityEngine;

public class ChangeCategory : MonoBehaviour
{
    public GameObject Panel_CollectBook;         // ���� �г�
    public GameObject[] CollectBookCategories;   // ���� ��������

    private const int InventoryBookIndex = 0;
    private const int EndingBookIndex = 1;

    // ���� �г� ����
    public void OpenCollectBook()
    {
        Panel_CollectBook.SetActive(true);
    }

    // ���� �г� �ݱ�
    public void CloseCollectBook()
    {
        Panel_CollectBook.SetActive(false);
    }

    // �κ��丮 ���� ����
    public void OpenInventoryBook()
    {
        SetActiveCategory(InventoryBookIndex);
        GameObject.FindWithTag("CollectBookManager").GetComponent<CombatItemBook>().ShowCombatItem();
    }

    // ���� ���� ����
    public void OpenEndingBook()
    {
        SetActiveCategory(EndingBookIndex);
    }

    // �ε��� ī�װ� Ȱ��ȭ �� ������ ī�װ� ��Ȱ��ȭ
    private void SetActiveCategory(int activeIndex)
    {
        for (int i = 0; i < CollectBookCategories.Length; i++)
        {
            CollectBookCategories[i].SetActive(i == activeIndex);
        }
    }
}
