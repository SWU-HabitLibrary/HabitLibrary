using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EndingBook : MonoBehaviour
{
    public GameObject[] ItemContents;
    public GameObject Item_Ending;              // 엔딩 아이템 프리팹

    public List<Ending> endings;                // 엔딩 도감 데이터
    public Sprite[] Item_Icons;                 // 아이템 스프라이트 배열
    public List<ItemData> endingPlayerDatas;    // 엔딩 플레이어 데이터

    private int curIndex = 0;
    private const int MaxPerPage = 3;

    public void Start()
    {
        endings = GameManager.Instance.endingObject.dataList;
        endingPlayerDatas = GameManager.Instance.EndingDatas;

        ShowEndingItem();           // 엔딩 도감을 보여줌
    }

    public string ShowPlayerData(int idx)
    {
        var playerData = endingPlayerDatas.FirstOrDefault(data => data.id == endings[idx].id);
        return playerData != null ? endings[idx].name : "?";
    }

    // 엔딩 도감과 보유 현황을 보여줌
    public void ShowEndingItem()
    {
        curIndex = 0; // 초기화
        DisableChildren();
        DisplayItems();
    }

    public void DisableChildren()
    {
        foreach (var item in ItemContents)
        {
            foreach (Transform child in item.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void DisplayItems()
    {
        for (int j = 0; j < ItemContents.Length; j++)
        {
            for (int i = 0; i < MaxPerPage && curIndex < endings.Count; i++)
            {
                SetItemContent(j, curIndex);
                curIndex++;
            }
        }
    }

    private bool HasPlayerData(int idx)
    {
        return endingPlayerDatas.Any(data => data.id == endings[idx].id);
    }

    private void SetItemContent(int contentIndex, int dataIndex)
    {
        GameObject newItem = Instantiate(Item_Ending, ItemContents[contentIndex].transform);

        if (HasPlayerData(dataIndex))
        {
            newItem.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Item_Icons[dataIndex];
            newItem.transform.GetChild(0).gameObject.SetActive(true);
            newItem.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            newItem.transform.GetChild(0).gameObject.SetActive(false);
            newItem.transform.GetChild(1).gameObject.GetComponent<Text>().text = "?";
            newItem.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void ShowNextEndingItem()
    {
        if (curIndex >= endings.Count) // 더 보여줄 아이템이 없다면 종료
            return;

        DisableChildren();
        DisplayItems();
    }


    public void ShowBeforeEndingItem()
    {
        if (curIndex - MaxPerPage * 2 < 0)
            return;

        curIndex -= MaxPerPage * 4;
        if (curIndex < 0)
            curIndex = 0;

        DisableChildren();
        DisplayItems();
    }
}
