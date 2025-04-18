using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UseItemSlot : MonoBehaviour, IDropHandler
{
    public UseItem UsingItem;
    
    public Image itemImg;
    public TextMeshProUGUI count;
    [SerializeField] int index;
    void Start()
    {
        ChangeIconText();
    }

    public void ChangeIconText()
    {
        if (UsingItem != null && UsingItem.ItemID != 0)
        {
            itemImg.color = Color.white;
            itemImg.sprite = GameData.Instance.AllItem[UsingItem.ItemID].icon;
            count.text = UsingItem.ItemCount.ToString();
        }
        else
        {
            itemImg.color = Color.clear;
            count.text = "";
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        DragItem dragItem = eventData.pointerDrag.GetComponent<DragItem>();
        if (dragItem != null)
        {
            if (dragItem.Item.type == Item.Type.Use)
            {
                UseItemSlot slot = Array.Find(FindFirstObjectByType<UsingItem>().slots, x => x.UsingItem == dragItem.Item);
                if(slot != null)
                {
                    slot.UsingItem = null;
                    slot.ChangeIconText();
                }
                PlayerScript player = FindFirstObjectByType<PlayerScript>();
                UsingItem = player.inventoryUseItems[dragItem.index];
                ChangeIconText();
            }
        }
    }
}
