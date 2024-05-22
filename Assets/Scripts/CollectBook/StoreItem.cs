using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    // 임시 상점 스크립트

    public GameObject ItemContent;
    public GameObject Item_Store;    // 상점 아이템 프리팹

    // 임시로 전투용 아이템만 판매
    public List<CombatItem> combatItems;    // 전투용 아이템 데이터
    public List<ItemData> combatPlayerDatas;    // 전투용 아이템에 대한 플레이어 데이터

    public Sprite[] Item_Icons;

    private int maxPerPage = 9;
    private List<GameObject> itemObjects = new List<GameObject>();

    public GameObject notice;
    public GameObject StorePanel;

    private void Start()
    {
        combatItems = GameManager.instance.combatItemObject.dataList;
        combatPlayerDatas = GameManager.instance.combatItemDatas;

        CreateItemObject();
    }

    public void OpenStorePanel()
    {
        StorePanel.gameObject.SetActive(true);
        ShowStoreItem();
    }

    private void CreateItemObject()
    {
        for (int i = 0; i < maxPerPage; i++)    // 미리 아이템 객체를 생성
        {
            GameObject newItem = Instantiate(Item_Store, ItemContent.transform);
            itemObjects.Add(newItem);
        }
    }

    public void ShowStoreItem()
    {
        // 아이템 중복 없이 랜덤으로 9개 선택
        List<int> randomItem = GetRandomIndex(combatItems.Count, maxPerPage);

        for (int i = 0; i < maxPerPage; i++)
        {
            GameObject newItem = itemObjects[i];
            int randomIdex = randomItem[i];
            newItem.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Item_Icons[randomIdex];
            newItem.transform.GetChild(1).gameObject.GetComponent<Text>().text = combatItems[randomIdex].name;
            newItem.transform.GetChild(2).gameObject.GetComponent<Text>().text = combatItems[randomIdex].explanation;
            newItem.transform.GetChild(3).gameObject.GetComponent<Text>().text = combatItems[randomIdex].price.ToString();

            // 버튼 이벤트 설정
            Button button = newItem.GetComponent<Button>();
            int itemIndex = randomIdex; // 클로저 이슈를 방지하기 위해 로컬 변수 사용
            button.onClick.RemoveAllListeners();    // 기존 이벤트 제거
            button.onClick.AddListener(() => BuyStoreItem(itemIndex));
        }
    }

    private List<int> GetRandomIndex(int itemTotalCount, int count)
    {
        List<int> randomList = new List<int>();

        while(randomList.Count < count)
        {
            int randomIndex = Random.Range(0, itemTotalCount);
            if (!randomList.Contains(randomIndex))
            {
                randomList.Add(randomIndex);
            }
        }

        return randomList;
    }

    public void BuyStoreItem(int itemIndex)
    {
        // 구매 함수

        int itemId = combatItems[itemIndex].id; // 아이템 데이터 베이스에서 인덱스 아이템 가져옴
        ItemData playerItemData = combatPlayerDatas.Find(item => item.id == itemId);    // 플레이어 아이템 데이터 리스트에서 해당 id의 아이템을 찾음

        if (playerItemData != null)
        {
            playerItemData.count++;     // id가 일치하는 아이템이 있으면 개수 증가
        }
        else
        {
            // 일치하는 아이템이 없으면 새로운 아이템 추가
            ItemData newItemData = new ItemData
            {
                id = itemId,
                count = 1
            };
            combatPlayerDatas.Add(newItemData);
            GameManager.instance.combatItemDatas = combatPlayerDatas;
        }

        // 구매 조건&골드 사용 기능 추가하기
        notice.SetActive(true);     // 구매 알림 활성화
    }
}
