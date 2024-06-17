using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CombatItemBook : MonoBehaviour
{
    public GameObject[] ItemContents;     
    public GameObject Item_CombatItem;           // 전투용 아이템 프리팹

    public List<CombatItem> combatItems;         // 전투용 아이템 도감 데이터
    public Sprite[] Item_Icons;                  // 아이템 스프라이트 배열
    public List<ItemData> combatPlayerDatas;     // 전투용 아이템 플레이어 보유 데이터

    private int curIndex = 0;
    private const int MaxPerPage = 3;

    public void Start()
    {
        combatItems = GameManager.Instance.combatItemObject.dataList;
        combatPlayerDatas = GameManager.Instance.CombatItemDatas;

        ShowCombatItem();       // 전투용 아이템 인벤토리를 보여줌
    }

    // 보유한 전투용 아이템 리스트를 보여줌
    public void ShowCombatItem()
    {
        curIndex = 0;           // 초기화
        DisableChildren();
        DisplayItems();
    }

    // 모든 아이템 비활성화
    public void DisableChildren()
    {
        foreach (GameObject item in ItemContents)
        {
            foreach (Transform child in item.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    // 필요한 개수만큼 아이템 활성화
    private void DisplayItems()
    {
        for (int j = 0; j < ItemContents.Length; j++)
        {
            for (int i = 0; i < MaxPerPage && curIndex < combatPlayerDatas.Count; i++)
            {
                if (curIndex < Item_Icons.Length)
                {
                    SetItemContent(j, curIndex);
                }
                curIndex++;
            }
        }
    }

    // 아이템 데이터 설정
    private void SetItemContent(int contentIndex, int dataIndex)
    {
        GameObject newItem = Instantiate(Item_CombatItem, ItemContents[contentIndex].transform);
        newItem.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Item_Icons[dataIndex];

        CombatItem item = combatItems.FirstOrDefault(x => x.id == combatPlayerDatas[dataIndex].id);
        if (item != null)
        {
            newItem.transform.GetChild(1).gameObject.GetComponent<Text>().text = item.name;
            newItem.transform.GetChild(2).gameObject.GetComponent<Text>().text = "보유 개수(임시) : " + combatPlayerDatas[dataIndex].count.ToString();
        }
    }

    // 도감 페이지 오른쪽 버튼
    public void ShowNextCombatItem()
    {
        if (combatPlayerDatas.Count - curIndex <= 0)  // 더 보여줄 아이템이 없다면 종료
            return;

        DisableChildren();
        DisplayItems();
    }

    // 도감 페이지 왼쪽 버튼
    public void ShowBeforeCombatItem()
    {
        int previousPageStartIndex = curIndex - MaxPerPage * 4;

        if (previousPageStartIndex < 0)
        {
            previousPageStartIndex = 0;
        }

        curIndex = previousPageStartIndex;
        DisableChildren();
        DisplayItems();
    }
}
