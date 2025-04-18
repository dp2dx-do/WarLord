using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField] bool isNPC;
    [SerializeField] Button[] btns;
    public int Page;
    private List<QuestSlot> slots;
    [SerializeField] Transform view;
    public UnityAction closed;
    public GameObject reward, accept;
    public TextMeshProUGUI rewardQuestName, rewardExp, rewardGold, acceptQuestName;
    public Button acceptBtn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        slots = new List<QuestSlot>();
        for (int i = 0; i < btns.Length; i++)
        {
            int index = i;
            btns[index].onClick.AddListener(delegate
            {
                if (TutorialManager.Instance==null)
                {
                    Page = index;
                    OnEnable();
                }
            });
        }
    }
    public void OnEnable()
    {
        if(TutorialManager.Instance!=null && TutorialManager.Instance.Page == 7)
        {
            Page = 1;
        }
        foreach (QuestSlot questSlot in slots)
        {
            questSlot.gameObject.SetActive(false);
        }
        PlayerScript player = FindFirstObjectByType<PlayerScript>();
        int slotindex = 0;
        foreach (Quest quest in player.quests)
        {
            QuestSO qs = QuestManager.instance.AllQuest[quest.QuestID];
            if ((int)quest.State == Page && qs.reqLevel <= player.Level)
            {
                if (slotindex >= slots.Count)
                {
                    if (isNPC)
                    {
                        slots.Add(Instantiate(Resources.Load<QuestSlot>("UI/QuestSlotNPC"), view));
                    }
                    else
                    {
                        slots.Add(Instantiate(Resources.Load<QuestSlot>("UI/QuestSlot"), view));
                    }
                }
                slots[slotindex].gameObject.SetActive(true);
                slots[slotindex].quest = quest;
                slots[slotindex].questSO = qs;
                slots[slotindex].isNPC = isNPC;
                slots[slotindex].TextChange();
                slotindex++;
            }
        }
    }

    private void OnDisable()
    {
        closed?.Invoke();
    }
}
