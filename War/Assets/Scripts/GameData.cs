using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerDataSave
{
    public List<PlayerData> playerDatas;
}

public class GameData : MonoBehaviour
{
    public static int[] UsingSkillPoints { get; private set; } = new int[] { 2, 3, 4, 5, 6, 8, 10, 13, 16, 19,
            23, 27, 31, 35, 40, 45, 51, 57, 63, 70,
        77, 84, 92, 100, 109};
    public int inventoryItemLength;
    [field: SerializeField]
    public PlayerDataSave save { get; private set; }
    int _index;
    public int Index
    {
        get { return _index; }
        set
        {
            _index = value;
            if (_index >= 0 && _index < save.playerDatas.Count)
            {
                playerData = save.playerDatas[_index];
            }
            else playerData = null;
        }
    }

    public PlayerData playerData;
    public static GameData Instance;
    public SortedDictionary<int, ItemData> AllItem { get; private set; }
    private void Awake()
    {
        Application.targetFrameRate = 120;
        Instance = this;
        AllItem = new SortedDictionary<int, ItemData>();
        ItemData[] itd = Resources.LoadAll<ItemData>("Scriptable/Item");
        foreach (ItemData item in itd)
        {
            AllItem.Add(item.ItemID, item);
        }
        DontDestroyOnLoad(gameObject);
        if (File.Exists(Application.persistentDataPath + "/Data.json"))
        {
            string fileText = File.ReadAllText(Application.persistentDataPath + "/Data.json");
            save = JsonUtility.FromJson<PlayerDataSave>(fileText);
            save.playerDatas.Sort();
            for(int i=0; i<save.playerDatas.Count; i++)
            {
                save.playerDatas[i].id = i;
            }
        }
        else
        {
            save = new PlayerDataSave();
            save.playerDatas = new List<PlayerData>();
        }
        Index = -1;
    }

    private void OnDestroy()
    {
        if (TutorialManager.Instance == null)
        {
            Debug.Log("Saves");
            if(Index>-1 && Index < save.playerDatas.Count)
            {
                save.playerDatas[Index] = playerData;
            }
            string fileText = JsonUtility.ToJson(save, true);
            File.WriteAllText(Application.persistentDataPath + "/Data.json", fileText);

        }
    }
}
