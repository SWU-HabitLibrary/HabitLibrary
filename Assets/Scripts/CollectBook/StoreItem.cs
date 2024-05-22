using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    // �ӽ� ���� ��ũ��Ʈ

    public GameObject ItemContent;
    public GameObject Item_Store;    // ���� ������ ������

    // �ӽ÷� ������ �����۸� �Ǹ�
    public List<CombatItem> combatItems;    // ������ ������ ������
    public List<ItemData> combatPlayerDatas;    // ������ �����ۿ� ���� �÷��̾� ������

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
        for (int i = 0; i < maxPerPage; i++)    // �̸� ������ ��ü�� ����
        {
            GameObject newItem = Instantiate(Item_Store, ItemContent.transform);
            itemObjects.Add(newItem);
        }
    }

    public void ShowStoreItem()
    {
        // ������ �ߺ� ���� �������� 9�� ����
        List<int> randomItem = GetRandomIndex(combatItems.Count, maxPerPage);

        for (int i = 0; i < maxPerPage; i++)
        {
            GameObject newItem = itemObjects[i];
            int randomIdex = randomItem[i];
            newItem.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Item_Icons[randomIdex];
            newItem.transform.GetChild(1).gameObject.GetComponent<Text>().text = combatItems[randomIdex].name;
            newItem.transform.GetChild(2).gameObject.GetComponent<Text>().text = combatItems[randomIdex].explanation;
            newItem.transform.GetChild(3).gameObject.GetComponent<Text>().text = combatItems[randomIdex].price.ToString();

            // ��ư �̺�Ʈ ����
            Button button = newItem.GetComponent<Button>();
            int itemIndex = randomIdex; // Ŭ���� �̽��� �����ϱ� ���� ���� ���� ���
            button.onClick.RemoveAllListeners();    // ���� �̺�Ʈ ����
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
        // ���� �Լ�

        int itemId = combatItems[itemIndex].id; // ������ ������ ���̽����� �ε��� ������ ������
        ItemData playerItemData = combatPlayerDatas.Find(item => item.id == itemId);    // �÷��̾� ������ ������ ����Ʈ���� �ش� id�� �������� ã��

        if (playerItemData != null)
        {
            playerItemData.count++;     // id�� ��ġ�ϴ� �������� ������ ���� ����
        }
        else
        {
            // ��ġ�ϴ� �������� ������ ���ο� ������ �߰�
            ItemData newItemData = new ItemData
            {
                id = itemId,
                count = 1
            };
            combatPlayerDatas.Add(newItemData);
            GameManager.instance.combatItemDatas = combatPlayerDatas;
        }

        // ���� ����&��� ��� ��� �߰��ϱ�
        notice.SetActive(true);     // ���� �˸� Ȱ��ȭ
    }
}
