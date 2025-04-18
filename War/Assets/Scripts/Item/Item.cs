using NUnit.Framework.Interfaces;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class Item
{
    public enum Type { None, Equip, Use}
    public Type type = Type.None;
    public int ItemID = 0;
    public int ItemCount;
}

[System.Serializable]
public class EquipItem : Item
{
    public int _star;
    public int Star
    {
        get { return _star; }
        set 
        {
            _star = value;
            if(_star > MaxStar)
            {
                _star = MaxStar;
            }
        }
    }

    public long AttackPower
    {
        get
        {
            EquipItemData eqData = (EquipItemData)GameData.Instance.AllItem[ItemID];
            if (eqData == null) return 0;
            if (eqData.Slot == 0)
            {
                return eqData.AttackPower + (eqData.itemClass + 1) * Star * 2;
            }
            else
            {
                return eqData.AttackPower + (eqData.itemClass + 1) * Star;
            }

        }
    }
    public int Defense 
    {
        get
        {
            EquipItemData eqData = (EquipItemData)GameData.Instance.AllItem[ItemID];
            if (eqData == null) return 0;

            if (eqData.Slot != 0)
            {
                return eqData.Defense + (eqData.itemClass + 1) * Star;
            }
            else
            {
                return eqData.Defense;
            }
        }
    }
    public long STR
    {
        get
        {
            EquipItemData eqData = (EquipItemData)GameData.Instance.AllItem[ItemID];
            if (eqData == null) return 0;

            return eqData.STR + (eqData.itemClass + 1) * Star;
        }
    }
    public long HP
    {
        get
        {
            EquipItemData eqData = (EquipItemData)GameData.Instance.AllItem[ItemID];
            if (eqData == null) return 0;

            return eqData.HP + (eqData.itemClass + 5) * Star;
        }
    }
    public int MaxStar
    {
        get
        {
            EquipItemData eqData = (EquipItemData)GameData.Instance.AllItem[ItemID];
            if (eqData == null) return 0;

            return Mathf.Min(eqData.itemClass * 3 + 7, 28);
        }
    }
    public string DescriptionText()
    {
        EquipItemData eqData = (EquipItemData)GameData.Instance.AllItem[ItemID];
        return $"Class : {eqData.itemClass}\nType : {+eqData.Slot}\nSTR : +{STR}\n최대 HP : +{HP}\n공격력 : +{AttackPower}\n방어력 : +{Defense}\n{Star}성 강화 적용\n(최대 {MaxStar}성)";
    }
    public EquipItem(int id)
    {
        ItemCount = 1;
        type = Type.Equip;
        ItemID = id;
        Star = 0;
    }
}
[System.Serializable]
public class UseItem : Item 
{
    public UseItem(int id)
    {
        type = Type.Use;
        ItemID = id;
    }
}
