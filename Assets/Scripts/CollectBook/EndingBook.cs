using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EndingBook : MonoBehaviour
{
    public GameObject[] ItemContents;
    public GameObject Item_Ending;              // ���� ������ ������

    public List<Ending> endings;                // ���� ���� ������
    public Sprite[] Item_Icons;                 // ������ ��������Ʈ �迭
    public List<ItemData> endingPlayerDatas;    // ���� �÷��̾� ������

    private int curIndex = 0;
    private const int MaxPerPage = 3;

    public void Start()
    {
        endings = GameManager.Instance.endingObject.dataList;
        endingPlayerDatas = GameManager.Instance.EndingDatas;

        ShowEndingItem();           // ���� ������ ������
    }

    public string ShowPlayerData(int idx)
    {
        var playerData = endingPlayerDatas.FirstOrDefault(data => data.id == endings[idx].id);
        return playerData != null ? endings[idx].name : "?";
    }

    // ���� ������ ���� ��Ȳ�� ������
    public void ShowEndingItem()
    {
        curIndex = 0; // �ʱ�ȭ
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
        if (curIndex >= endings.Count) // �� ������ �������� ���ٸ� ����
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
