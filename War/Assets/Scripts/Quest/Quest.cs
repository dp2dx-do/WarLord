using System;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public int QuestID;
    public enum QuestState { Startable, Progress, Completed }
    public QuestState State;
    public int Current;
    public Quest(int id)
    {
        QuestID = id;
        State = QuestState.Startable;
        Current = 0;
    }
}
