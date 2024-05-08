using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class EndingBook : MonoBehaviour
{
    public GameObject endingContents;
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

    public void ShowEndingItem()
    {
        // ���� ���� ������ŭ �����Ͽ� �����ִ� �Լ�

        // ���� �ε������� �� �������� �ִ� ǥ�� ����ŭ �������� ǥ��

        for (int i = 0; i < maxPerPage && curIndex < endings.Count; i++)
        {
            GameObject newItem = Instantiate(Item_Ending, endingContents.transform);
            //newItem.transform.GetChild(0).gameObject.GetComponent<Text>().text = endings[curIndex].name;
            newItem.transform.GetChild(0).gameObject.GetComponent<Text>().text = ShowPlayerData(curIndex);

            curIndex++;
        }
    }

    public void ShowNextEndingItem()
    {
        // ������ �ѱ� ��ư�� ������ ���� ���� �������� �����ִ� �Լ�

        if (endings.Count - curIndex <= 0)  // ���� �� ������ �������� ���ٸ�
            return; // ����

        if (curIndex % maxPerPage == 0)  // ���� �������� �� á�ٸ�
        {
            int leftItem = endings.Count - curIndex;
            if (leftItem >= maxPerPage)  // �������� maxPerPage �̻� ���Ҵٸ�
            {
                // ������ ������Ʈ ��Ȱ��
                for (int i = 0; i < maxPerPage; i++)
                {
                    //endingContents.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = endings[curIndex].name;
                    endingContents.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = ShowPlayerData(curIndex);
                    curIndex++;
                }
            }
            else    // �������� maxPerPage ���� ���� ���Ҵٸ�
            {
                // �ʿ��� ��ŭ ��Ȱ�� & ���� ��ŭ ����(��Ȱ��ȭ)
                for (int i = 0; i < maxPerPage; i++)
                {
                    if (i < leftItem)
                    {
                        //endingContents.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = endings[curIndex].name;
                        endingContents.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = ShowPlayerData(curIndex);
                        curIndex++;
                    }
                    else
                        endingContents.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }    

    public void ShowBeforeEndingItem()
    {
        // ������ ���� ��ư�� ������ ���� ���� �������� �����ִ� �Լ�

        if (curIndex - maxPerPage <= 0)
            return;
        
        if (curIndex % maxPerPage == 0)  // ���� �������� �� á�ٸ�
        {
            curIndex -= maxPerPage * 2; // �� ������ �����ۼ���ŭ �ε��� ����

            for (int i = 0; i < maxPerPage; i++)
            {
                //endingContents.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = endings[curIndex].name;
                endingContents.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = ShowPlayerData(curIndex);

                curIndex++;
            }
        }
        else    // ���� �������� �� ���� �ʾҴٸ�(== ������ ���������)
        {
            // ��Ȱ��ȭ�Ͽ� ���� ������ Ȱ��ȭ
            curIndex -= maxPerPage * 2; // �� ������ �����ۼ���ŭ �ε��� ����

            for (int i = 0; i < maxPerPage; i++)
            {
                int curCnt = endings.Count % maxPerPage;    // Ȱ��ȭ �Ǿ��ִ� ������ ����
                if (i >= curCnt)
                {
                    endingContents.transform.GetChild(i).gameObject.SetActive(true);
                    curIndex++;
                }

                //endingContents.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = endings[curIndex].name;
                endingContents.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = ShowPlayerData(curIndex);
                curIndex++;
            }
        }
    }
}
