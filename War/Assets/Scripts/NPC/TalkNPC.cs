using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class SaveTalkText 
{
    public TalkContents[] contents;
}

[System.Serializable]
public class TalkContents
{
    public string WhoSays;
    [TextArea]
    public string WhatSays;
}

public class TalkNPC : NPCScript
{
    int _page;
    public int Page
    {
        get
        {
            return _page;
        }
        set
        {
            _page = value;
            SetDialogue(value);
        }
    }
    public int maxPage
    {
        get
        {
            return talkText.contents.Length - 1;
        }
    }
    Coroutine talkCo;
    string path;
    [SerializeField] SaveTalkText talkText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        base.Start();
        action += () =>
        {
            Page = 0;
        };
        path = Application.dataPath + "/TalkNPC/" + gameObject.name + ".json";
        if (File.Exists(path))
        {
            talkText = JsonUtility.FromJson<SaveTalkText>(File.ReadAllText(path));
        }
        else
        {
            File.WriteAllText(path, JsonUtility.ToJson(talkText, true));
        }
    }
    public override void SetDialogue(int page)
    {
        TalkDialogue talk = PopUp.GetComponentInChildren<TalkDialogue>();
        talk.npc = this;
        if (talkCo != null)
        {
            StopCoroutine(talkCo);
            talkCo = null;
        }
        talkCo = StartCoroutine(talk.Talk(talkText.contents[page].WhatSays));
        talk.WhoSays.text = talkText.contents[page].WhoSays;
        talk.page.text = $"{page + 1} / {maxPage + 1}";
    }

}
