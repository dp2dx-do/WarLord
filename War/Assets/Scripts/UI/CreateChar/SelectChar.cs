using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;



public class SelectChar : MonoBehaviour
{
    [SerializeField] Button[] btns;
    [SerializeField] Button startBtn;
    [SerializeField] TextMeshProUGUI[] charInfos;
    [SerializeField] DeleteChar[] deleteChars;
    [SerializeField] Transform[] charPos;
    [SerializeField] GameObject CreateChar;
    [SerializeField] PreviewChar Knight, Mage;
    [SerializeField] PreviewChar[] previewChars;
    [SerializeField] Transform moveToPos;
    [SerializeField] int charIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        charIndex = -1;
        charInfos = new TextMeshProUGUI[6];
        deleteChars = new DeleteChar[6];
        previewChars = new PreviewChar[6];
        for (int i=0; i<6; i++)
        {
            charInfos[i] = btns[i].GetComponentInChildren<TextMeshProUGUI>();
            deleteChars[i] = btns[i].GetComponentInChildren<DeleteChar>();
            if (i < GameData.Instance.save.playerDatas.Count)
            {
                deleteChars[i].index = i;
                PlayerData player = GameData.Instance.save.playerDatas[i];
                if (player.charClass == CharClass.Knight)
                {
                    previewChars[i] = Instantiate(Knight, charPos[i]);
                }
                else if(player.charClass == CharClass.Mage)
                {
                    previewChars[i] = Instantiate(Mage, charPos[i]);
                }
                charInfos[i].text = $"{player.charName}\nLv.{player.Level}";
            }
            else
            {
                deleteChars[i].gameObject.SetActive(false);
                charInfos[i].text = "캐릭터 생성";
            }
        }
        for (int i = 0; i < btns.Length; i++) 
        {
            int index = i;
            btns[index].onClick.AddListener(delegate
            {
                startBtn.gameObject.SetActive(true);
                if (previewChars[index] != null)
                {
                    if (charIndex != index)
                    {
                        if (charIndex > -1)
                        {
                            previewChars[charIndex].Move(previewChars[charIndex].orgPos);
                        }
                        charIndex = index;
                        previewChars[index].Move(moveToPos);
                    }
                }
                else
                {
                    CreateChar.SetActive(true);
                }
            });
        }
        startBtn.onClick.AddListener(delegate
        {
            if (!previewChars[charIndex].myAnim.GetBool("IsRun"))
            {
                GameData.Instance.Index = charIndex;
                if (GameData.Instance.playerData != null)
                {
                    Fading.Instance.Fade(2);
                }
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
