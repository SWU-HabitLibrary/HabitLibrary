using UnityEngine;

public class ChangeCategory : MonoBehaviour
{
    public GameObject Panel_CollectBook;         // 도감 패널
    public GameObject[] CollectBookCategories;   // 도감 페이지들

    private const int InventoryBookIndex = 0;
    private const int EndingBookIndex = 1;

    // 도감 패널 열기
    public void OpenCollectBook()
    {
        Panel_CollectBook.SetActive(true);
    }

    // 도감 패널 닫기
    public void CloseCollectBook()
    {
        Panel_CollectBook.SetActive(false);
    }

    // 인벤토리 도감 열기
    public void OpenInventoryBook()
    {
        SetActiveCategory(InventoryBookIndex);
        GameObject.FindWithTag("CollectBookManager").GetComponent<CombatItemBook>().ShowCombatItem();
    }

    // 엔딩 도감 열기
    public void OpenEndingBook()
    {
        SetActiveCategory(EndingBookIndex);
    }

    // 인덱스 카테고리 활성화 및 나머지 카테고리 비활성화
    private void SetActiveCategory(int activeIndex)
    {
        for (int i = 0; i < CollectBookCategories.Length; i++)
        {
            CollectBookCategories[i].SetActive(i == activeIndex);
        }
    }
}
