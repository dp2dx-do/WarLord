using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueChange : MonoBehaviour
{
    private PlayerScript player_;
    public static SliderValueChange instance;
    public Slider[] sliders;
    public TextMeshProUGUI[] slidertexts;
    public TextMeshProUGUI LevelText;
    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject); 
        player_ = FindFirstObjectByType<PlayerScript>();
    }

    // Update is called once per frame
    public void HPChange()
    {
        sliders[0].value = (float)player_.HP / player_.MaxHP;
        slidertexts[0].text = $"{player_.HP} / {player_.MaxHP}";

    }
    public void MPChange()
    {
        sliders[1].value = (float)player_.MP / player_.MaxMP;
        slidertexts[1].text = $"{player_.MP} / {player_.MaxMP}";

    }
    public void EXPChange()
    {
        sliders[2].value = (float)player_.EXP / player_.MaxExp;
        slidertexts[2].text = $"{player_.EXP} / {player_.MaxExp}";

    }
    public void LevelChange()
    {
        LevelText.text = player_.Level.ToString();
    }
}
