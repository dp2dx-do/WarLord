using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasicCanvas : MonoBehaviour
{
    public static BasicCanvas Instance;
    public Canvas skillIconCanvas, HPMPEXPCanvas, TimeGoldCanvas, MinimapCanvas, DragItemCanvas, TutorialCanvas;
    public int BossDiff;
    public GameObject Dialogue;
    [SerializeField] private Button[] BotRightButtons;
    public GameObject[] BotRightButtonOpenObjects; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
        Instantiate(skillIconCanvas);
        Instantiate(HPMPEXPCanvas);
        Instantiate(TimeGoldCanvas);
        Instantiate(MinimapCanvas);
        DragItemCanvas = Instantiate(DragItemCanvas);
        if (GameData.Instance.playerData.isTutorialOpen)
        {
            Instantiate(TutorialCanvas);
        }
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(DragItemCanvas.gameObject);
        for(int i=0; i<BotRightButtons.Length; i++)
        {
            int index = i;
            BotRightButtons[index].onClick.AddListener(delegate{
                if (BotRightButtonOpenObjects[index] != null && TutorialManager.Instance==null)
                    BotRightButtonOpenObjects[index].SetActive(true);
            });
        }
    }
}
