using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour, IPointerClickHandler
{
    public bool isNPC;
    public Quest quest;
    public QuestSO questSO;
    public Button btn;
    public TextMeshProUGUI title, desctiption;
    public UnityAction action;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void TextChange()
    {
        title.text = questSO.questName;
        desctiption.text = $"{questSO.questDescription}\n{quest.Current} / {questSO.questCount}";
    }

    

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isNPC)
        {
            QuestUI questui = GetComponentInParent<QuestUI>();
            if (quest.State == Quest.QuestState.Startable)
            {
                questui.accept.gameObject.SetActive(true);
                questui.acceptBtn.onClick.AddListener(delegate
                {
                    quest.State = Quest.QuestState.Progress;
                    QuestSlot2.Instance.Change();
                    action?.Invoke();
                    questui.acceptBtn.onClick.RemoveAllListeners();
                    questui.accept.gameObject.SetActive(false);
                    questui.OnEnable();
                });
                questui.acceptQuestName.text = questSO.questName;
            }
            if (quest.State == Quest.QuestState.Progress && quest.Current >= questSO.questCount)
            {
                quest.State = Quest.QuestState.Completed;
                PlayerScript player = FindFirstObjectByType<PlayerScript>();
                player.Gold += questSO.RewardGold;
                player.EXP += questSO.RewardExp;
                QuestSlot2.Instance.Change();

                questui.reward.SetActive(true);
                questui.rewardQuestName.text = questSO.questName;
                questui.rewardExp.text = $"{questSO.RewardExp.ToString("N0")} EXP";
                questui.rewardGold.text = $"{questSO.RewardGold.ToString("N0")} Gold";
                action?.Invoke();
                questui.OnEnable();
            }
        }
    }
}
