using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillLevelUI : MonoBehaviour
{
    public static SkillLevelUI instance;
    public TextMeshProUGUI SkillPointText;
    public TextMeshProUGUI[] SkillLevelDamageText;
    [SerializeField] Button[] SkillLevelChange;
    
    private int[] Skilllevels;
    private int LeftSkillPoint;
    private float[] SkillDamage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        instance = this;
        
        SkillDamage = new float[7];
        SkillLevelChange = GetComponentsInChildren<Button>();
        SkillLevelDamageText = GetComponentsInChildren<TextMeshProUGUI>();
        PlayerScript player = FindFirstObjectByType<PlayerScript>();

        for (int i=0; i<7; i++)
        {
            int index = i;
            SkillDamage[index] = (float)player.useSkills[index].Multipler * 100f;
            SkillLevelChange[index*2].onClick.AddListener(delegate
            {
                if (Skilllevels[index] < GameData.UsingSkillPoints.Length && LeftSkillPoint >= GameData.UsingSkillPoints[Skilllevels[index]])
                {
                    player.UsedSkillPoint += GameData.UsingSkillPoints[Skilllevels[index]];
                    Skilllevels[index]++;
                    OnEnable();
                }
            });
            SkillLevelChange[index *2 + 1].onClick.AddListener(delegate {
                if (Skilllevels[index] > 0)
                {
                    Skilllevels[index]--;
                    player.UsedSkillPoint -= GameData.UsingSkillPoints[Skilllevels[index]];
                    OnEnable();
                }
            });
        }
    }
    void OnEnable()
    {
        PlayerScript player = FindFirstObjectByType<PlayerScript>();
        if (player != null)
        {
            Skilllevels = player.SkillLevels;
            LeftSkillPoint = player.SkillPoint - player.UsedSkillPoint;
            SkillPointText.text = LeftSkillPoint + " / " + player.SkillPoint;
            for (int i = 0; i < 7; i++)
            {
                SkillLevelDamageText[i * 2 + 1].text = $"Lv. {Skilllevels[i]}";
                SkillLevelDamageText[i * 2 + 2].text = $"{(SkillDamage[i] * (1f + 0.1f * Skilllevels[i])).ToString("N0")} %";

            }
        }
    }
    private void OnDisable()
    {
        PlayerScript player = FindFirstObjectByType<PlayerScript>();
        player.SkillLevels = Skilllevels;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
