using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class CombatItemBook : MonoBehaviour
{
    public GameObject[] ItemContents;
    public GameObject Item_CombatItem;  // ������ ������ ������
    public List<CombatItem> combatItems;    // ������ ������ ������
    public List<ItemData> combatPlayerDatas;    // ������ �����ۿ� ���� �÷��̾� ������
    public Sprite[] Item_Icons; 

    private int curIndex = 0;
    private int maxPerPage = 3;

    public void Start()
    {
        combatItems = GameManager.instance.combatItemObject.dataList;
        combatPlayerDatas = GameManager.instance.combatItemDatas;
        ShowCombatItem();   // ������ ������ �κ��丮�� ������
    }

    public void ShowCombatItem()
    {
        // ������ ������ �κ��丮 ������ŭ �����Ͽ� �����ִ� �Լ�

        // ���� �ε������� �� �������� �ִ� ǥ�� ����ŭ �������� ǥ��
        for (int j = 0; j < ItemContents.Length; j++)
        {
            for (int i = 0; i < maxPerPage && curIndex < combatPlayerDatas.Count; i++)
            {
                GameObject newItem = Instantiate(Item_CombatItem, ItemContents[j].transform);
                newItem.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Item_Icons[curIndex];

                CombatItem item = combatItems.FirstOrDefault(x => x.id == combatPlayerDatas[curIndex].id);
                if (item != null)
                {
                    newItem.transform.GetChild(1).gameObject.GetComponent<Text>().text = item.name;
                    newItem.transform.GetChild(2).gameObject.GetComponent<Text>().text = "���� ����(�ӽ�) : " + combatPlayerDatas[curIndex].count.ToString();
                }

                curIndex++;
            }
        }
    }


    public void ShowNextCombatItem()
    {
        // ������ �ѱ� ��ư�� ������ ���� ������ �������� �����ִ� �Լ�

        if (combatPlayerDatas.Count - curIndex <= 0)  // ���� �� ������ �������� ���ٸ�
            return; // ����

        if (curIndex % (maxPerPage * 2) == 0)  // ���� �������� �� á�ٸ�
        {
            int leftItem = combatPlayerDatas.Count - curIndex;
            if (leftItem >= (maxPerPage * 2))  // �������� maxPerPage �̻� ���Ҵٸ�
            {
                // ������ ������Ʈ ��Ȱ��
                for (int j = 0; j < ItemContents.Length; j++)
                {
                    for (int i = 0; i < maxPerPage; i++)
                    {
                        CombatItem item = combatItems.FirstOrDefault(x => x.id == combatPlayerDatas[curIndex].id);
                        if (item != null)
                        {
                            ItemContents[j].transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().sprite = Item_Icons[curIndex];
                            ItemContents[j].transform.GetChild(i).GetChild(1).gameObject.GetComponent<Text>().text = item.name;
                            //ItemContents[j].transform.GetChild(i).GetChild(2).gameObject.GetComponent<Text>().text = item.explanation;
                            ItemContents[j].transform.GetChild(i).GetChild(2).gameObject.GetComponent<Text>().text = "���� ����(�ӽ�) : " + combatPlayerDatas[curIndex].count.ToString();
                        }
                        curIndex++;
                    }
                }
            }
            else    // �������� maxPerPage ���� ���� ���Ҵٸ�
            {
                // �ʿ��� ��ŭ ��Ȱ�� & ���� ��ŭ ����(��Ȱ��ȭ)

                for (int j = 0; j < ItemContents.Length; j++)
                {
                    leftItem = combatPlayerDatas.Count - curIndex;

                    for (int i = 0; i < maxPerPage; i++)
                    {
                        if (i < leftItem)
                        {
                            CombatItem item = combatItems.FirstOrDefault(x => x.id == combatPlayerDatas[curIndex].id);
                            if (item != null)
                            {
                                ItemContents[j].transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().sprite = Item_Icons[curIndex];
                                ItemContents[j].transform.GetChild(i).GetChild(1).gameObject.GetComponent<Text>().text = item.name;
                                //ItemContents[j].transform.GetChild(i).GetChild(2).gameObject.GetComponent<Text>().text = item.explanation;
                                ItemContents[j].transform.GetChild(i).GetChild(2).gameObject.GetComponent<Text>().text = "���� ����(�ӽ�) : " + combatPlayerDatas[curIndex].count.ToString();
                            }
                            curIndex++;
                        }
                        else
                            ItemContents[j].transform.GetChild(i).gameObject.SetActive(false);
                    }

                    leftItem = leftItem - maxPerPage > maxPerPage ? leftItem - maxPerPage : 0;
                }
            }
        }
    }

    public void ShowBeforeCombatItem()
    {
        // ������ ���� ��ư�� ������ ���� ������ �������� �����ִ� �Լ�

        if (curIndex - (maxPerPage * 2) - 1 < 0)
            return;

        if (curIndex % (maxPerPage * 2) == 0)  // ���� �������� �� á�ٸ�
        {
            curIndex -= (maxPerPage * 2) * 2; // ���� �������� ���� �ε����� �̵�

            for (int j = 0; j < ItemContents.Length; j++)
            {
                for (int i = 0; i < maxPerPage; i++)
                {
                    CombatItem item = combatItems.FirstOrDefault(x => x.id == combatPlayerDatas[curIndex].id);
                    if (item != null)
                    {
                        ItemContents[j].transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().sprite = Item_Icons[curIndex];
                        ItemContents[j].transform.GetChild(i).GetChild(1).gameObject.GetComponent<Text>().text = item.name;
                        // ItemContents[j].transform.GetChild(i).GetChild(2).gameObject.GetComponent<Text>().text = item.explanation;  // ������ ����
                        ItemContents[j].transform.GetChild(i).GetChild(2).gameObject.GetComponent<Text>().text = "���� ����(�ӽ�) : " + combatPlayerDatas[curIndex].count.ToString();
                    }
                    curIndex++;
                }
            }
        }
        else    // ���� �������� �� ���� �ʾҴٸ�(== ������ ���������)
        {
            // ��Ȱ��ȭ�Ͽ� ���� ������ Ȱ��ȭ

            int curCnt = combatPlayerDatas.Count % (maxPerPage * 2);    // Ȱ��ȭ �Ǿ��ִ� ������ ����
            curIndex -= (maxPerPage * 2) + curCnt;    // �� ������ �����ۼ���ŭ �ε��� ����
            
            for (int j = 0; j < ItemContents.Length; j++)
            {
                for (int i = 0; i < maxPerPage; i++)
                {
                    if (i >= curCnt)
                    {
                        ItemContents[j].transform.GetChild(i).gameObject.SetActive(true);
                    }


                    CombatItem item = combatItems.FirstOrDefault(x => x.id == combatPlayerDatas[curIndex].id);
                    if (item != null)
                    {
                        ItemContents[j].transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().sprite = Item_Icons[curIndex];
                        ItemContents[j].transform.GetChild(i).GetChild(1).gameObject.GetComponent<Text>().text = item.name;
                        //ItemContents[j].transform.GetChild(i).GetChild(2).gameObject.GetComponent<Text>().text = item.explanation;
                        ItemContents[j].transform.GetChild(i).GetChild(2).gameObject.GetComponent<Text>().text = "���� ����(�ӽ�) : " + combatPlayerDatas[curIndex].count.ToString();
                    }
                    curIndex++;
                }

                if (j != ItemContents.Length - 1)
                    curCnt -= (maxPerPage > curCnt) ? maxPerPage - 1 : curCnt - 1;
            }
        }
    }
}
