using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public RectTransform rect;
    PlayerScript player;
    public DragItemSlot[] dragItemSlots;
    [SerializeField] private Button[] btn;
    public int page;
    public GameObject Info;
    public TextMeshProUGUI infoText, nameText;
    void Awake()
    {
        Instance = this;
        dragItemSlots = new DragItemSlot[GameData.Instance.inventoryItemLength];
        page = 0;
        for(int i=0; i<GameData.Instance.inventoryItemLength; i++)
        {
            dragItemSlots[i] = Instantiate(Resources.Load<DragItemSlot>("UI/DragItemSlot"), rect);
        }
        for (int i = 0; i < dragItemSlots.Length; i++) 
        {
            dragItemSlots[i].index = i;
        }
        btn[0].onClick.AddListener(delegate { page = 0; StartCoroutine(Wait()); });
        btn[1].onClick.AddListener(delegate { page = 1; StartCoroutine(Wait()); });

    }

    public void OnEnable()
    {
        StartCoroutine(Wait());
        Info.SetActive(false);
    }
    IEnumerator Wait()
    {
        if(player == null) player = FindFirstObjectByType<PlayerScript>();
        while (player == null) yield return null;
        while (player.inventoryUseItems == null) yield return null;
        
        for (int i = 0; i < dragItemSlots.Length; i++)
        {
            if(page == 0)
            {
                if (player.inventoryEquipItems[i] != null)
                {
                    if (player.inventoryEquipItems[i].ItemID != 0)
                    {
                        if (dragItemSlots[i].drItem == null)
                        {
                            dragItemSlots[i].drItem = Instantiate(Resources.Load<DragItem>("UI/DragItem"), dragItemSlots[i].transform);
                        }
                        dragItemSlots[i].drItem.Item = player.inventoryEquipItems[i];
                        dragItemSlots[i].drItem.index = i;
                        dragItemSlots[i].drItem.Initialize();
                    }
                    else
                    {
                        if (dragItemSlots[i].drItem != null)
                        {
                            Destroy(dragItemSlots[i].drItem.gameObject);
                        }
                    }
                }
                else
                {
                    if (dragItemSlots[i].drItem != null)
                    {
                        Destroy(dragItemSlots[i].drItem.gameObject);
                    }
                }
            }
            if(page == 1)
            {
                if (player.inventoryUseItems[i] != null)
                {
                    if (player.inventoryUseItems[i].ItemID != 0)
                    {
                        if (dragItemSlots[i].drItem == null)
                        {
                            dragItemSlots[i].drItem = Instantiate(Resources.Load<DragItem>("UI/DragItem"), dragItemSlots[i].transform);
                        }
                        dragItemSlots[i].drItem.Item = player.inventoryUseItems[i];
                        dragItemSlots[i].drItem.index = i;

                        dragItemSlots[i].drItem.Initialize();
                    }
                    else
                    {
                        if (dragItemSlots[i].drItem != null)
                        {
                            Destroy(dragItemSlots[i].drItem.gameObject);
                        }
                    }
                }
                else
                {
                    if (dragItemSlots[i].drItem != null)
                    {
                        Destroy(dragItemSlots[i].drItem.gameObject);
                    }
                }
            }
        }
    }

    public void ChangePlayerData(int i, int j)
    {
        if(page == 0)
        {
            EquipItem eq = player.inventoryEquipItems[i];
            player.inventoryEquipItems[i] = player.inventoryEquipItems[j];
            player.inventoryEquipItems[j] = eq;
        }
        if(page == 1)
        {
            UseItem u = player.inventoryUseItems[i];
            player.inventoryUseItems[i] = player.inventoryUseItems[j];
            player.inventoryUseItems[j] = u;
        }
    }
    public void DeletePlayerData(int i)
    {
        if (page == 0)
        {
            player.inventoryEquipItems[i].ItemID = 0;
        }
        if (page == 1)
        {
            player.inventoryUseItems[i].ItemID = 0;
        }
    }

    private void Update()
    {
        if (Info.activeSelf)
        {
            Info.transform.position = Input.mousePosition;
        }
    }
}
