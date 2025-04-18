using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Objects/Quest")]
public class QuestSO : ScriptableObject
{
    public int questID;
    public int reqLevel;
    [TextArea]
    public string questName, questDescription;
    public bool AnyMonster;
    public List<int> normalMonsterID;
    public int BossMonsterID;
    public int BossDifficult;
    public int questCount;
    public int RewardGold, RewardExp;
}
