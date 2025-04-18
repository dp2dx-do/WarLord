using UnityEngine;

[CreateAssetMenu(fileName = "EquipItemData", menuName = "Scriptable Objects/EquipItemData")]
public class EquipItemData : ItemData
{
    public int itemClass;
    public long AttackPower;
    public long STR;
    public long HP;
    public int Defense;
    [Range(0,4)]public int Slot;
    [ContextMenu("Sett")]
    public void Set()
    {
        ItemID = itemClass * 10 + Slot;
    }
}
