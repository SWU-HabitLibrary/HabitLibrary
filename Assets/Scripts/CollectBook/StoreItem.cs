using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    // 임시 상점 스크립트

    public GameObject ItemContent;
    public GameObject Item_Store;    // 상점 아이템 프리팹

    public List<CombatItem> combatItems;    // 전투용 아이템 데이터
    public List<ItemData> combatPlayerDatas;    // 전투용 아이템에 대한 플레이어 데이터

    public Sprite[] Item_Icons;

    private const int MaxPerPage = 9;
    private List<GameObject> itemObjects = new List<GameObject>();

    public GameObject notice;
    public GameObject StorePanel;

    private void Start()
    {
        combatItems = GameManager.Instance.combatItemObject.dataList;
        combatPlayerDatas = GameManager.Instance.CombatItemDatas;

        CreateItemObject();
    }

    public void OpenStorePanel()
    {
        StorePanel.gameObject.SetActive(true);
        ShowStoreItem();
    }

    private void CreateItemObject()
    {
        for (int i = 0; i < MaxPerPage; i++)    // 아이템 객체 미리 생성
        {
            GameObject newItem = Instantiate(Item_Store, ItemContent.transform);
            itemObjects.Add(newItem);
        }
    }

    // 아이템 중복 없이 랜덤으로 9개 보여줌
    public void ShowStoreItem()
    {
        List<int> randomItemIndices = GetRandomIndices(combatItems.Count, MaxPerPage);

        for (int i = 0; i < MaxPerPage; i++)
        {
            int randomIndex = randomItemIndices[i];
            UpdateItemObject(itemObjects[i], randomIndex);
        }
    }


    private List<int> GetRandomIndices(int itemCount, int count)
    {
        List<int> randomList = new List<int>();

        while (randomList.Count < count)
        {
            int randomIndex = Random.Range(0, itemCount);
            if (!randomList.Contains(randomIndex))
            {
                randomList.Add(randomIndex);
            }
        }

        return randomList;
    }

    private void UpdateItemObject(GameObject itemObject, int itemIndex)
    {
        CombatItem combatItem = combatItems[itemIndex];
        itemObject.transform.GetChild(0).GetComponent<Image>().sprite = Item_Icons[itemIndex];
        itemObject.transform.GetChild(1).GetComponent<Text>().text = combatItem.name;
        itemObject.transform.GetChild(2).GetComponent<Text>().text = combatItem.explanation;
        itemObject.transform.GetChild(3).GetComponent<Text>().text = combatItem.price.ToString();

        // 버튼 이벤트 설정
        Button button = itemObject.GetComponent<Button>();
        button.onClick.RemoveAllListeners(); // 기존 이벤트 제거
        button.onClick.AddListener(() => BuyStoreItem(itemIndex));
    }

    public void BuyStoreItem(int itemIndex)
    {
        // 구매 함수

        int itemId = combatItems[itemIndex].id;         // 아이템 데이터 베이스에서 인덱스 아이템 가져옴
        ItemData playerItemData = combatPlayerDatas.Find(item => item.id == itemId);    // 플레이어 아이템 데이터 리스트에서 해당 id의 아이템을 찾음

        if (playerItemData != null)
        {
            playerItemData.count++;     // id가 일치하는 아이템이 있으면 개수 증가
        }
        else
        {
            // 일치하는 아이템이 없으면 새로운 아이템 추가
            ItemData newItemData = new ItemData(itemId, 1);
            combatPlayerDatas.Add(newItemData);
            GameManager.Instance.CombatItemDatas = combatPlayerDatas;
        }

        // 구매 조건&골드 사용 기능 추가하기
        notice.SetActive(true);     // 구매 알림 활성화
    }
}
