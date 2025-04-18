using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChaosDungeon : MonoBehaviour
{
    public static ChaosDungeon Instance;
    public PlayerScript Player { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }
    public void EnterPlayer(PlayerScript player)
    {
        Player = player;
        Spawner[] spawners = FindObjectsByType<Spawner>(FindObjectsSortMode.None);
        foreach (Quest q in Player.quests)
        {
            if (q.State == Quest.QuestState.Progress)
            {
                foreach (Spawner spawner in spawners)
                {
                    spawner.OnSpawn += i =>
                    {
                        if (QuestManager.instance.AllQuest[q.QuestID].AnyMonster || QuestManager.instance.AllQuest[q.QuestID].normalMonsterID.Contains(i))
                        {
                            if(q.Current< QuestManager.instance.AllQuest[q.QuestID].questCount)
                            q.Current++;
                        }
                    };
                }
            }
        }
    }
    public void GiveExpGold(int exp, int gold)
    {
        Player.EXP += exp;
        Player.Gold += gold;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
