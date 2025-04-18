using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour, IDropHandler
{
    public static Shop Instance { get; private set; }
    public List<ItemData> sellItemDatas;
    public Transform content;
    public List<ShopSlot> sellItemSlot;
    public ShopSlot selected;
    private PlayerScript player; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        Instance = this;
        foreach (ItemData item in GameData.Instance.AllItem.Values) 
        {
            ShopSlot s = Instantiate(Resources.Load<ShopSlot>("UI/ShopSlot"), content);
            s.itemData = item;
            s.Initialize();
            sellItemSlot.Add(s);
        }
    }
    private void OnEnable()
    {
        player = FindFirstObjectByType<PlayerScript>();
        if (Inventory.Instance == null || !Inventory.Instance.isActiveAndEnabled)
        {
            BasicCanvas.Instance.BotRightButtonOpenObjects[2].SetActive(true);
        }
        BasicCanvas.Instance.BotRightButtonOpenObjects[2].transform.position = new Vector3(Screen.width * 0.75f, Screen.height * 0.5f);
    }
    public void OnDrop(PointerEventData eventData)
    {
        DragItem dragItem = eventData.pointerDrag.GetComponent<DragItem>();
        if (dragItem != null) 
        {
            player.Gold += dragItem.Item.ItemCount * Mathf.FloorToInt(GameData.Instance.AllItem[dragItem.Item.ItemID].Price * 0.5f);
            Inventory.Instance.DeletePlayerData(dragItem.index);
            Destroy(dragItem.gameObject);
        }
    }
}