using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossMap : MonoBehaviour
{
    public static BossMap instance;
    public BossMonster boss;
    public Slider hpBar;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI failed;
    [SerializeField] GameObject clearWindow;
    [SerializeField] TextMeshProUGUI clearExp, clearGold;
    [SerializeField] Button clearButton;
    public PlayerScript Player { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        boss = FindFirstObjectByType<BossMonster>();
        boss.Init();
        HPChange();
    }

    // Update is called once per frame
    public void HPChange()
    {

        hpBar.value = Mathf.Clamp((float)boss.HP / boss.MaxHP, 0f, 1f);
        hpText.text = boss.HP + " / " + boss.MaxHP;
    }
    public void EnterPlayer(PlayerScript player)
    {
        Player = player;
    }
    public void DiePlayer(PlayerScript player)
    {
        StartCoroutine(Failed());
    }
    public IEnumerator Failed()
    {
        failed.gameObject.SetActive(true);
        float t = 0;
        while(t<1)
        {
            t += Time.deltaTime * 0.4f;
            failed.color = Color.Lerp(Color.clear, Color.red, t);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        Fading.Instance.Fade(2);
        Fading.Instance.pos = Fading.Instance.BeforePos;
    }
    public IEnumerator Clear()
    {
        Player.Gold += boss.ClearGoldArray[boss.Difficulty];
        Player.EXP += boss.ClearEXPArray[boss.Difficulty];
        yield return new WaitForSeconds(3f);
        clearWindow.SetActive(true);
        clearExp.text = $"{boss.ClearEXPArray[boss.Difficulty].ToString("N0")} EXP";
        clearGold.text = $"{boss.ClearGoldArray[boss.Difficulty].ToString("N0")} Gold";
        clearButton.onClick.AddListener(delegate
        {
            Fading.Instance.Fade(2);
            Fading.Instance.pos = Fading.Instance.BeforePos;
        });
    }
}
