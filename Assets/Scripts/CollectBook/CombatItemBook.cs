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
    public GameObject Item_CombatItem;  // 전투용 아이템 프리팹
    public List<CombatItem> combatItems;    // 전투용 아이템 데이터
    public List<ItemData> combatPlayerDatas;    // 전투용 아이템에 대한 플레이어 데이터
    public Sprite[] Item_Icons; 

    private int curIndex = 0;
    private int maxPerPage = 3;

    public void Start()
    {
        combatItems = GameManager.instance.combatItemObject.dataList;
        combatPlayerDatas = GameManager.instance.combatItemDatas;
        ShowCombatItem();   // 전투용 아이템 인벤토리를 보여줌
    }

    public void ShowCombatItem()
    {
        // 전투용 아이템 인벤토리 개수만큼 생성하여 보여주는 함수

        // 현재 인덱스부터 한 페이지의 최대 표시 수만큼 아이템을 표시
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
                    newItem.transform.GetChild(2).gameObject.GetComponent<Text>().text = "보유 개수(임시) : " + combatPlayerDatas[curIndex].count.ToString();
                }

                curIndex++;
            }
        }
    }


    public void ShowNextCombatItem()
    {
        // 페이지 넘김 버튼을 누르면 다음 전투용 아이템을 보여주는 함수

        if (combatPlayerDatas.Count - curIndex <= 0)  // 만약 더 보여줄 아이템이 없다면
            return; // 종료

        if (curIndex % (maxPerPage * 2) == 0)  // 이전 페이지가 꽉 찼다면
        {
            int leftItem = combatPlayerDatas.Count - curIndex;
            if (leftItem >= (maxPerPage * 2))  // 아이템이 maxPerPage 이상 남았다면
            {
                // 생성한 오브젝트 재활용
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
                            ItemContents[j].transform.GetChild(i).GetChild(2).gameObject.GetComponent<Text>().text = "보유 개수(임시) : " + combatPlayerDatas[curIndex].count.ToString();
                        }
                        curIndex++;
                    }
                }
            }
            else    // 아이템이 maxPerPage 보다 적게 남았다면
            {
                // 필요한 만큼 재활용 & 남은 만큼 숨김(비활성화)

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
                                ItemContents[j].transform.GetChild(i).GetChild(2).gameObject.GetComponent<Text>().text = "보유 개수(임시) : " + combatPlayerDatas[curIndex].count.ToString();
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
        // 페이지 이전 버튼을 누르면 이전 전투용 아이템을 보여주는 함수

        if (curIndex - (maxPerPage * 2) - 1 < 0)
            return;

        if (curIndex % (maxPerPage * 2) == 0)  // 현재 페이지가 꽉 찼다면
        {
            curIndex -= (maxPerPage * 2) * 2; // 이전 페이지의 시작 인덱스로 이동

            for (int j = 0; j < ItemContents.Length; j++)
            {
                for (int i = 0; i < maxPerPage; i++)
                {
                    CombatItem item = combatItems.FirstOrDefault(x => x.id == combatPlayerDatas[curIndex].id);
                    if (item != null)
                    {
                        ItemContents[j].transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>().sprite = Item_Icons[curIndex];
                        ItemContents[j].transform.GetChild(i).GetChild(1).gameObject.GetComponent<Text>().text = item.name;
                        // ItemContents[j].transform.GetChild(i).GetChild(2).gameObject.GetComponent<Text>().text = item.explanation;  // 아이템 설명
                        ItemContents[j].transform.GetChild(i).GetChild(2).gameObject.GetComponent<Text>().text = "보유 개수(임시) : " + combatPlayerDatas[curIndex].count.ToString();
                    }
                    curIndex++;
                }
            }
        }
        else    // 현재 페이지가 꽉 차지 않았다면(== 마지막 페이지라면)
        {
            // 비활성화하여 숨긴 아이템 활성화

            int curCnt = combatPlayerDatas.Count % (maxPerPage * 2);    // 활성화 되어있는 아이템 개수
            curIndex -= (maxPerPage * 2) + curCnt;    // 한 페이지 아이템수만큼 인덱스 감소
            
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
                        ItemContents[j].transform.GetChild(i).GetChild(2).gameObject.GetComponent<Text>().text = "보유 개수(임시) : " + combatPlayerDatas[curIndex].count.ToString();
                    }
                    curIndex++;
                }

                if (j != ItemContents.Length - 1)
                    curCnt -= (maxPerPage > curCnt) ? maxPerPage - 1 : curCnt - 1;
            }
        }
    }
}
