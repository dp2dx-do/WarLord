using TMPro;
using UnityEngine;

public class BossMonster : Monster
{
    [SerializeField] protected long[] HPArray;
    [SerializeField] protected long[] AttackPowerArray;
    public int[] ClearGoldArray;
    public int[] ClearEXPArray;

    public int Difficulty;
    private void Awake()
    {
        delay = 5f;
        Difficulty = BasicCanvas.Instance.BossDiff;        
        MaxHP = HPArray[Difficulty];
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        base.Start();
        AttackPower = AttackPowerArray[Difficulty];
    }

    public void Init()
    {
        changeHP += () => BossMap.instance.HPChange();
    }
    // Update is called once per frame

    public override void HitScript(long dmg)
    {
        base.HitScript(dmg);
        if (BossMap.instance != null)
        {
            BossMap.instance.HPChange();
        }
    }
    public override void DieScript()
    {
        base.DieScript();
        state = States.Die;
        myAnim.SetTrigger("Die");
        
        foreach (Quest quest in BossMap.instance.Player.quests)
        {
            if (quest.State == Quest.QuestState.Progress)
            {
                QuestSO SO = QuestManager.instance.AllQuest[quest.QuestID];
                if (SO.BossMonsterID == monsterID && SO.BossDifficult <= Difficulty && quest.Current < SO.questCount)
                {
                    quest.Current++;
                }
            }
        }
        QuestSlot2.Instance.TextChange();
        if (BossMap.instance != null)
        {
            StartCoroutine(BossMap.instance.Clear());
        }
    }


}
