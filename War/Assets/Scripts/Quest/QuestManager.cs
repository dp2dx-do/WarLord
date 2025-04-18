using System.Collections.Generic;
using System.IO;
using UnityEngine;






public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public SortedDictionary<int, QuestSO> AllQuest { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        AllQuest = new SortedDictionary<int, QuestSO>();
        QuestSO[] quests = Resources.LoadAll<QuestSO>("Scriptable/Quest");
        foreach (QuestSO quest in quests)
        {
            AllQuest.Add(quest.questID, quest);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
