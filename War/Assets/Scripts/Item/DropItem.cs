using Unity.VisualScripting;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public ItemData itemData;
    public int itemCount;
    public Transform pos;

    public void Initialize()
    {
        Instantiate(itemData.dropItemResource, pos);
    }

  
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
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
                                player.inventoryUseItems[i].ItemCount += itemCount;
                                if(player.inventoryUseItems[i].ItemCount > itemData.MaxItemCount)
                                {
                                    player.inventoryUseItems[i].ItemCount = itemData.MaxItemCount;
                                }

                            }
                        }
                    }
                }
                if (!isIn)
                {
                    for (int i = 0; i < player.inventoryUseItems.Length; i++)
                    {
                        if (player.inventoryUseItems[i] == null)
                        {
                            player.inventoryUseItems[i] = new UseItem(itemData.ItemID);
                            player.inventoryUseItems[i].ItemCount = itemCount;
                            break;
                        }
                        else if (player.inventoryUseItems[i].ItemID == 0)
                        {
                            player.inventoryUseItems[i].type = Item.Type.Use;
                            player.inventoryUseItems[i].ItemID = itemData.ItemID;
                            player.inventoryUseItems[i].ItemCount = itemCount;
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < player.inventoryEquipItems.Length; i++)
                {
                    if (player.inventoryEquipItems[i] == null || player.inventoryEquipItems[i].ItemID ==0)
                    {
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
            Destroy(gameObject);
        }
    }
}
