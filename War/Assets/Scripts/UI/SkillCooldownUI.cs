using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownUI : MonoBehaviour
{
    public static SkillCooldownUI Instance;
    private PlayerScript player;
    public Image[] skillIcons;
    public TextMeshProUGUI[] cooltext;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<PlayerScript>();
        DontDestroyOnLoad(transform.parent.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < skillIcons.Length; i++) {
            skillIcons[i].fillAmount = player.CoolTimes[i] / player.OriginalCoolTime[i];
            float cool = player.CoolTimes[i];
            if (cool > 1)
            {
                cooltext[i].text = cool.ToString("F0");
            }
            else if (cool > 0)
            {
                cooltext[i].text = cool.ToString("F1");
            }
            else cooltext[i].text = "";
        }
        
    }
}
