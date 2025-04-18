using UnityEngine;

[CreateAssetMenu(fileName = "UseItemData", menuName = "Scriptable Objects/UseItemData")]
public class UseItemData : ItemData
{
    [TextArea] public string description;
    public int Recovery;
    [Range(0f, 1f)] public float RecoveryPercent;

    [ContextMenu("Sett")]
    public void Set()
    {
        ItemID = 1000000000 + Recovery * 1000 + (int)(RecoveryPercent * 1000);
    }
}
