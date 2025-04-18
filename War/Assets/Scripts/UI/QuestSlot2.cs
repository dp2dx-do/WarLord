using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestSlot2 : MonoBehaviour
{
    public static QuestSlot2 Instance;
    public Image[] images;
    public Quest[] quests;
    public QuestSO[] questSOs;
    public TextMeshProUGUI[] texts;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        images = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<TextMeshProUGUI>();
        quests = new Quest[5];
        questSOs = new QuestSO[5];
        Change();
    }
    public void Change()
    {
        int index = 0;
        PlayerScript player = FindFirstObjectByType<PlayerScript>();
        foreach (Quest s in player.quests)
        { 
            if(s.State == Quest.QuestState.Progress)
            {
                quests[index] = s;
                questSOs[index] = QuestManager.instance.AllQuest[s.QuestID];
                images[index].gameObject.SetActive(true);
                index++;
            }
        }
        while (index < 5)
        {
            quests[index] = null;
            questSOs[index] = null;
            images[index].gameObject.SetActive(false);
            index++;
        }
        TextChange();
    }

    public void TextChange()
    {
        for(int i=0; i<5; i++)
        {
            if (texts[i].IsActive())
                texts[i].text = $"{questSOs[i].questName}\n{quests[i].Current} / {questSOs[i].questCount}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
