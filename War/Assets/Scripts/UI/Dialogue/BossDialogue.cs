using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BossDialogue : Dialogue
{
    public TextMeshProUGUI bossName;
    [SerializeField] Transform Diff;
    [SerializeField] Button[] buttons;
    public int MapTo;
    public Vector3 pos;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
        buttons = Diff.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[index].onClick.AddListener(delegate
            {
                BasicCanvas.Instance.BossDiff = index;
                Fading.Instance.Fade(MapTo);
                Fading.Instance.pos = pos;
                gameObject.SetActive(false);
            });
        }
        gameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
