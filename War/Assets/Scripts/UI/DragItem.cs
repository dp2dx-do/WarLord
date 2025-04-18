
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler,
    IPointerExitHandler
{
    public int index;
    public Rect rect;
    public int clicked;
    public Item Item;
    public Image iconImg;
    public TextMeshProUGUI countText;
    public Transform orgParent = null;
    public Vector2 range;
    public void OnBeginDrag(PointerEventData eventData)
    {
        orgParent = transform.parent;
        transform.SetParent(BasicCanvas.Instance.DragItemCanvas.transform);
        transform.SetAsLastSibling();
        transform.GetComponent<Image>().raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(orgParent);
        transform.localPosition = Vector3.zero;
        GetComponent<Image>().raycastTarget = true;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Inventory.Instance.Info.SetActive(true);
        clicked = 0;
        ItemData itemd = GameData.Instance.AllItem[Item.ItemID];
        Inventory.Instance.nameText.text = itemd.Name;
        if(itemd.Type == ItemData.ItemType.Equip)
        {
            EquipItem eq = (EquipItem)Item;
            Inventory.Instance.infoText.text = eq.DescriptionText();
        }
        else if(itemd.Type == ItemData.ItemType.Use)
        {
            UseItemData use = (UseItemData)itemd;
            Inventory.Instance.infoText.text = use.description;
        }

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Inventory.Instance.Info.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        if(++clicked > 1)
        {
            if (GameData.Instance.AllItem[Item.ItemID].Type == ItemData.ItemType.Equip)
            {
                PlayerScript player = FindFirstObjectByType<PlayerScript>();
                if (player != null)
                {
                    EquipItem eqi = player.inventoryEquipItems[index];
                    EquipItemData ed = (EquipItemData)GameData.Instance.AllItem[Item.ItemID];
                    player.inventoryEquipItems[index] = player.equipItems[ed.Slot];
                    player.equipItems[ed.Slot] = eqi;
                    Inventory.Instance.OnEnable();
                    player.PlayerStatChange();
                }
            }
        }
    }


    public void ChangeParent(Transform parent)
    {
        orgParent = parent;
    }

    public void Initialize()
    {
        iconImg.sprite = GameData.Instance.AllItem[Item.ItemID].icon;
        if(Item.type == Item.Type.Equip)
        {
            countText.text = ((EquipItem)Item).Star.ToString();
        }
        else
        {
            countText.text= Item.ItemCount.ToString();
        }
        rect = Inventory.Instance.GetComponent<RectTransform>().rect;
        range.x = rect.width / 2;
        range.y = rect.height / 2;
    }

}