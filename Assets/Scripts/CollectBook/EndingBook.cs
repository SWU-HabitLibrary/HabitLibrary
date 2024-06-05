using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class EndingBook : MonoBehaviour
{
    public GameObject[] ItemContents;
    public GameObject Item_Ending;  // ���� ������ ������
    public List<Ending> endings;    // ���� ������
    public List<ItemData> endingPlayerDatas;    // ���� �����Ϳ� ���� �÷��̾� ������

    private int curIndex = 0;
    private int maxPerPage = 3;

    public void Start()
    {
        endings = GameManager.instance.endingObject.dataList;
        endingPlayerDatas = GameManager.instance.endingDatas;
        ShowEndingItem();   // ���� ������ ������
    }

    public string ShowPlayerData(int _idx)
    {
        string str = "?";

        for (int i = 0; i < endingPlayerDatas.Count; i++)
        {
            if (endings[_idx].id == endingPlayerDatas[i].id)
            {
                str = endings[_idx].name;
                break;
            }
        }

        return str;
    }

 /*   public void DisableChildren()
    {
        foreach (GameObject item in ItemContents)
        {
            // Get all children of the current item
            foreach (Transform child in item.transform)
            {
                // Disable the child GameObject
                child.gameObject.SetActive(false);
            }
        }
    }*/

    public void ShowEndingItem()
    {
        // ���� ���� ������ŭ �����Ͽ� �����ִ� �Լ�

        // ���� �ε������� �� �������� �ִ� ǥ�� ����ŭ �������� ǥ��
        for (int j = 0; j < ItemContents.Length; j++)
        {
            for (int i = 0; i < maxPerPage && curIndex < endings.Count; i++)
            {
                GameObject newItem = Instantiate(Item_Ending, ItemContents[j].transform);
                newItem.transform.GetChild(0).gameObject.GetComponent<Text>().text = ShowPlayerData(curIndex);

                curIndex++;
            }
        }
    }

    public void ShowNextEndingItem()
    {
        // ������ �ѱ� ��ư�� ������ ���� ���� �������� �����ִ� �Լ�

        if (endings.Count - curIndex <= 0)  // ���� �� ������ �������� ���ٸ�
            return; // ����

        if (curIndex % (maxPerPage * 2) == 0)  // ���� �������� �� á�ٸ�
        {
            int leftItem = endings.Count - curIndex;
            if (leftItem >= (maxPerPage * 2))  // �������� maxPerPage �̻� ���Ҵٸ�
            {
                // ������ ������Ʈ ��Ȱ��
                for (int j = 0; j < ItemContents.Length; j++)
                {
                    // ������ ������Ʈ ��Ȱ��
                    for (int i = 0; i < maxPerPage; i++)
                    {
                        //endingContents.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = endings[curIndex].name;
                        ItemContents[j].transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = ShowPlayerData(curIndex);
                        curIndex++;
                    }
                }
            }
            else    // �������� maxPerPage ���� ���� ���Ҵٸ�
            {
                // �ʿ��� ��ŭ ��Ȱ�� & ���� ��ŭ ����(��Ȱ��ȭ)
                for (int j = 0; j < ItemContents.Length; j++)
                {
                    leftItem = endings.Count - curIndex;

                    for (int i = 0; i < maxPerPage; i++)
                    {
                        if (i < leftItem)
                        {
                            //endingContents.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = endings[curIndex].name;
                            ItemContents[j].transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = ShowPlayerData(curIndex);
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

    public void ShowBeforeEndingItem()
    {
        // ������ ���� ��ư�� ������ ���� ���� �������� �����ִ� �Լ�

        /*if (curIndex - maxPerPage <= 0)
            return;*/

        if (curIndex - (maxPerPage * 2) < 1)
            return;

        if (curIndex % (maxPerPage * 2) == 0)  // ���� �������� �� á�ٸ�
        {
            curIndex -= (maxPerPage * 2) * 2; // ���� �������� ���� �ε����� �̵�

            for (int j = 0; j < ItemContents.Length; j++)
            {
                for (int i = 0; i < maxPerPage; i++)
                {
                    //endingContents.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = endings[curIndex].name;
                    ItemContents[j].transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = ShowPlayerData(curIndex);

                    curIndex++;
                }
            }
        }
        else    // ���� �������� �� ���� �ʾҴٸ�(== ������ ���������)
        {
            // ��Ȱ��ȭ�Ͽ� ���� ������ Ȱ��ȭ

            int curCnt = endings.Count % (maxPerPage * 2);    // Ȱ��ȭ �Ǿ��ִ� ������ ����
            curIndex -= (maxPerPage * 2) + curCnt;    // �� ������ �����ۼ���ŭ �ε��� ����

            for (int j = 0; j < ItemContents.Length; j++)
            {                                              
                for (int i = 0; i < maxPerPage; i++)
                {
                    if (i >= curCnt)
                    {
                        ItemContents[j].transform.GetChild(i).gameObject.SetActive(true);
                        curIndex++;
                    }

                    //endingContents.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = endings[curIndex].name;
                    ItemContents[j].transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = ShowPlayerData(curIndex);
                    curIndex++;
                }

                if (j != ItemContents.Length - 1)
                    curCnt -= (maxPerPage > curCnt) ? maxPerPage - 1 : curCnt - 1;
            }
        }
    }
}
