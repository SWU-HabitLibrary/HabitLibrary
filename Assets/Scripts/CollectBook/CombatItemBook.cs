using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CombatItemBook : MonoBehaviour
{
    public GameObject[] ItemContents;     
    public GameObject Item_CombatItem;           // ������ ������ ������

    public List<CombatItem> combatItems;         // ������ ������ ���� ������
    public Sprite[] Item_Icons;                  // ������ ��������Ʈ �迭
    public List<ItemData> combatPlayerDatas;     // ������ ������ �÷��̾� ���� ������

    private int curIndex = 0;
    private const int MaxPerPage = 3;

    public void Start()
    {
        combatItems = GameManager.Instance.combatItemObject.dataList;
        combatPlayerDatas = GameManager.Instance.CombatItemDatas;

        ShowCombatItem();       // ������ ������ �κ��丮�� ������
    }

    // ������ ������ ������ ����Ʈ�� ������
    public void ShowCombatItem()
    {
        curIndex = 0;           // �ʱ�ȭ
        DisableChildren();
        DisplayItems();
    }

    // ��� ������ ��Ȱ��ȭ
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

    // �ʿ��� ������ŭ ������ Ȱ��ȭ
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

    // ������ ������ ����
    private void SetItemContent(int contentIndex, int dataIndex)
    {
        GameObject newItem = Instantiate(Item_CombatItem, ItemContents[contentIndex].transform);
        newItem.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Item_Icons[dataIndex];

        CombatItem item = combatItems.FirstOrDefault(x => x.id == combatPlayerDatas[dataIndex].id);
        if (item != null)
        {
            newItem.transform.GetChild(1).gameObject.GetComponent<Text>().text = item.name;
            newItem.transform.GetChild(2).gameObject.GetComponent<Text>().text = "���� ����(�ӽ�) : " + combatPlayerDatas[dataIndex].count.ToString();
        }
    }

    // ���� ������ ������ ��ư
    public void ShowNextCombatItem()
    {
        if (combatPlayerDatas.Count - curIndex <= 0)  // �� ������ �������� ���ٸ� ����
            return;

        DisableChildren();
        DisplayItems();
    }

    // ���� ������ ���� ��ư
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
