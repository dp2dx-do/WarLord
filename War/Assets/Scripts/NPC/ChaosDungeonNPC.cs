using UnityEngine;

public class ChaosDungeonNPC : NPCScript
{
    public int[] MapTo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        base.Start();
        action += () =>
        {
            ChaosDungeonDialogue move = PopUp.GetComponentInChildren<ChaosDungeonDialogue>();
            move.MapTo = MapTo;
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
