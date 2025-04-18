
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image BackGround;
    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI itemInfoText;
    public ItemData itemData;
    public bool isSelected;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isSelected)
        {
            if(Shop.Instance.selected != null)
            {
                Shop.Instance.selected.isSelected = false;
                Shop.Instance.selected.BackGround.color = Color.clear;
            }
            isSelected = true;
            BackGround.color = Color.cyan;
            Shop.Instance.selected = this;
        }
        else
        {
            PlayerScript player = FindFirstObjectByType<PlayerScript>();
            if (player.Gold >= itemData.Price)
            {
                bool isPurchased = false;
                if (itemData.Type != ItemData.ItemType.Equip)
                {
                    bool isIn = false;
                    for (int i = 0; i < player.inventoryUseItems.Length; i++)
                    {
                        if (player.inventoryUseItems[i] != null)
                        {
                            if (player.inventoryUseItems[i].ItemID != 0)
                            {
                                if (itemData.ItemID == player.inventoryUseItems[i].ItemID)
                                {
                                    isIn = true;
                                    isPurchased = true;
                                    player.inventoryUseItems[i].ItemCount += 1;
                                    if (player.inventoryUseItems[i].ItemCount > itemData.MaxItemCount)
                                    {
                                        player.inventoryUseItems[i].ItemCount = itemData.MaxItemCount;
                                        isPurchased = false;
                                    }

                                }
                            }
                        }
                    }
                    if (!isIn)
                    {
                        for (int i = 0; i < player.inventoryUseItems.Length; i++)
                        {
                            if (player.inventoryUseItems[i] == null || player.inventoryUseItems[i].ItemID == 0)
                            {
                                isPurchased = true;
                                player.inventoryUseItems[i] = new UseItem(itemData.ItemID);
                                player.inventoryUseItems[i].ItemCount = 1;


                                break;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < player.inventoryEquipItems.Length; i++)
                    {
                        if (player.inventoryEquipItems[i] == null || player.inventoryEquipItems[i].ItemID == 0)
                        {
                            isPurchased = true;
                            player.inventoryEquipItems[i] = new EquipItem(itemData.ItemID);
                            break;
                        }
                    }
                }
                if (Inventory.Instance != null)
                {
                    if (Inventory.Instance.gameObject.activeSelf)
                    {
                        Inventory.Instance.OnEnable();
                    }
                }
                if (isPurchased)
                {
                    player.Gold -= itemData.Price;
                    isSelected = false;
                    BackGround.color = Color.clear;
                }
            }
        }
    }

    public void Initialize()
    {
        if (itemData != null)
        {
            itemInfoText.text = $"{itemData.Name}\n{itemData.Price.ToString("N0")} °ñµå";
            itemIcon.sprite = itemData.icon;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
}
