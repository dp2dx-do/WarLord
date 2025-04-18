using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipUI : MonoBehaviour
{
    [SerializeField] Image[] equipImg;
    [SerializeField] TextMeshProUGUI playerInfo;
    [SerializeField] TextMeshProUGUI StatAtk;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        PlayerScript player = FindFirstObjectByType<PlayerScript>();
        EquipItemSlot[] itemSlots = GetComponentsInChildren<EquipItemSlot>();
        for (int i = 0; i < player.equipItems.Length; i++) 
        {
            if (player.equipItems[i].ItemID != 0)
            {
                equipImg[i].sprite = GameData.Instance.AllItem[player.equipItems[i].ItemID].icon;
                itemSlots[i].item = player.equipItems[i];
            }
        }
        StatAtk.text = ((long)(player.StatATK * 0.9)).ToString("N0") + "\n~ " + player.StatATK.ToString("N0");
        playerInfo.text = $"{player.AttackPower.ToString("N0")}\n{player.STR.ToString("N0")}\n{player.Defense.ToString("N0")}\n{player.MaxHP.ToString("N0")}";
    }

    // Update is called once per frame
    void Update()
    {
        StringBuilder stringBuilder = new StringBuilder("a");
        stringBuilder.Append("b");
        string temp = stringBuilder.ToString();
    }
}
