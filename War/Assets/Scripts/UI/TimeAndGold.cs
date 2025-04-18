using System;
using TMPro;
using UnityEngine;

public class TimeAndGold : MonoBehaviour
{
    public static TimeAndGold Instance;
    [SerializeField] TextMeshProUGUI TimeText, GoldText;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(transform.parent.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        TimeText.text = DateTime.Now.ToString("HH:mm:ss");
    }

    public void GoldChange(int gold)
    {
        GoldText.text = gold.ToString("N0");
    }
}
