using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    [field: SerializeField] public int ItemID { get; protected set; }
    public enum ItemType { Equip, Use}
    public ItemType Type;
    public string Name;
    public GameObject dropItemResource;
    public Sprite icon;
    public int MaxItemCount;
    public int Price;
    
}
