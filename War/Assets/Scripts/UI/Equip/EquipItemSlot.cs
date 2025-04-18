using TMPro;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public EquipItem item;
    bool isOn;
    public int SlotIndex;
    public GameObject Info;
    public TextMeshProUGUI infoText, nameText;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item.ItemID != 0)
        {
            EquipItemData itemData = (EquipItemData)GameData.Instance.AllItem[item.ItemID];
            isOn = true;
            Info.SetActive(true);
            infoText.text = item.DescriptionText();
            nameText.text = itemData.Name;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOn = false;
        Info.SetActive(false); 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            Info.transform.position = Input.mousePosition;
        }
    }
}
