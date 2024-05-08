using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class EndingBook : MonoBehaviour
{
    public GameObject endingContents;
    public GameObject Item_Ending;  // 엔딩 아이템 프리팹
    public List<Ending> endings;    // 엔딩 데이터
    public List<ItemData> endingPlayerDatas;    // 엔딩 데이터에 대한 플레이어 데이터

    private int curIndex = 0;
    private int maxPerPage = 3;

    public void Start()
    {
        endings = GameManager.instance.endingObject.dataList;
        endingPlayerDatas = GameManager.instance.endingDatas;
        ShowEndingItem();   // 엔딩 도감을 보여줌
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
        // 엔딩 도감 개수만큼 생성하여 보여주는 함수

        // 현재 인덱스부터 한 페이지의 최대 표시 수만큼 아이템을 표시

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
        // 페이지 넘김 버튼을 누르면 다음 엔딩 아이템을 보여주는 함수

        if (endings.Count - curIndex <= 0)  // 만약 더 보여줄 아이템이 없다면
            return; // 종료

        if (curIndex % maxPerPage == 0)  // 이전 페이지가 꽉 찼다면
        {
            int leftItem = endings.Count - curIndex;
            if (leftItem >= maxPerPage)  // 아이템이 maxPerPage 이상 남았다면
            {
                // 생성한 오브젝트 재활용
                for (int i = 0; i < maxPerPage; i++)
                {
                    //endingContents.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = endings[curIndex].name;
                    endingContents.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = ShowPlayerData(curIndex);
                    curIndex++;
                }
            }
            else    // 아이템이 maxPerPage 보다 적게 남았다면
            {
                // 필요한 만큼 재활용 & 남은 만큼 숨김(비활성화)
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
        // 페이지 이전 버튼을 누르면 이전 엔딩 아이템을 보여주는 함수

        if (curIndex - maxPerPage <= 0)
            return;
        
        if (curIndex % maxPerPage == 0)  // 현재 페이지가 꽉 찼다면
        {
            curIndex -= maxPerPage * 2; // 한 페이지 아이템수만큼 인덱스 감소

            for (int i = 0; i < maxPerPage; i++)
            {
                //endingContents.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = endings[curIndex].name;
                endingContents.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text = ShowPlayerData(curIndex);

                curIndex++;
            }
        }
        else    // 현재 페이지가 꽉 차지 않았다면(== 마지막 페이지라면)
        {
            // 비활성화하여 숨긴 아이템 활성화
            curIndex -= maxPerPage * 2; // 한 페이지 아이템수만큼 인덱스 감소

            for (int i = 0; i < maxPerPage; i++)
            {
                int curCnt = endings.Count % maxPerPage;    // 활성화 되어있는 아이템 개수
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
