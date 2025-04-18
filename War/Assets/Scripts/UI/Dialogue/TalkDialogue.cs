using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TalkDialogue : Dialogue, IPointerClickHandler
{
    public TextMeshProUGUI content;
    public TextMeshProUGUI WhoSays;
    public TextMeshProUGUI page;
    public TalkNPC npc;
    [SerializeField] Button[] btn;
    public TextMeshProUGUI nextPage;

    public IEnumerator Talk(string str)
    {        
        nextPage.gameObject.SetActive(false);
        List<char> chars = new List<char>();
        int index = 0;
        while(index < str.Length)
        {
            chars.Add(str[index++]);
            content.text = string.Concat(chars.ToArray());
            yield return null;
        }
        nextPage.gameObject.SetActive(true);

    }
    private void OnDisable()
    {
        FindFirstObjectByType<PlayerScript>().IsTalk = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (nextPage.gameObject.activeSelf)
        {
            if (npc.Page < npc.maxPage) npc.Page++;
            else 
            {
                gameObject.SetActive(false);
                FindFirstObjectByType<TutorialPage2>()?.EndPage();
            }
        }
        
    }
}
