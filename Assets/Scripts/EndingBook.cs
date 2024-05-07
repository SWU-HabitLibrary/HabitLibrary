using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingBook : MonoBehaviour
{
    public GameObject Panel_CollectBook;    // 도감 패널
    public GameObject endingContents;
    public GameObject Item_Ending;  // 엔딩 아이템 프리팹
    public List<Ending> endings;    // 엔딩 데이터

    private int curIndex = 0;

    public void Start()
    {
        endings = GameManager.instance.endingObject.dataList;
    }

    public void ShowEndingItem()
    {
        // 엔딩 도감 개수만큼 생성하는 함수

        // 현재 인덱스부터 다음 4개의 아이템을 표시
        for (int i = 0; i < 4; i++)
        {
            for (int j = curIndex; j < endings.Count; j++)
            {
                GameObject newItem = Instantiate(Item_Ending);
            }
        }

    }

    public void OpenCollectBook()
    {
        Panel_CollectBook.SetActive(true);
    }

    public void CloseCollectBook()
    {
        Panel_CollectBook.SetActive(false);
    }

}
