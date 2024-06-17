using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    // �ӽ� ���� ��ũ��Ʈ

    public GameObject ItemContent;
    public GameObject Item_Store;    // ���� ������ ������

    public List<CombatItem> combatItems;    // ������ ������ ������
    public List<ItemData> combatPlayerDatas;    // ������ �����ۿ� ���� �÷��̾� ������

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
        for (int i = 0; i < MaxPerPage; i++)    // ������ ��ü �̸� ����
        {
            GameObject newItem = Instantiate(Item_Store, ItemContent.transform);
            itemObjects.Add(newItem);
        }
    }

    // ������ �ߺ� ���� �������� 9�� ������
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

        // ��ư �̺�Ʈ ����
        Button button = itemObject.GetComponent<Button>();
        button.onClick.RemoveAllListeners(); // ���� �̺�Ʈ ����
        button.onClick.AddListener(() => BuyStoreItem(itemIndex));
    }

    public void BuyStoreItem(int itemIndex)
    {
        // ���� �Լ�

        int itemId = combatItems[itemIndex].id;         // ������ ������ ���̽����� �ε��� ������ ������
        ItemData playerItemData = combatPlayerDatas.Find(item => item.id == itemId);    // �÷��̾� ������ ������ ����Ʈ���� �ش� id�� �������� ã��

        if (playerItemData != null)
        {
            playerItemData.count++;     // id�� ��ġ�ϴ� �������� ������ ���� ����
        }
        else
        {
            // ��ġ�ϴ� �������� ������ ���ο� ������ �߰�
            ItemData newItemData = new ItemData(itemId, 1);
            combatPlayerDatas.Add(newItemData);
            GameManager.Instance.CombatItemDatas = combatPlayerDatas;
        }

        // ���� ����&��� ��� ��� �߰��ϱ�
        notice.SetActive(true);     // ���� �˸� Ȱ��ȭ
    }
}
