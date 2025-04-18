using UnityEngine;

public class BossNPC : NPCScript
{
    public string bossName;
    public int Map;
    public Vector3 Pos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        base.Start();
        action += () =>
        {
            BossDialogue move = PopUp.GetComponentInChildren<BossDialogue>();
            move.MapTo = Map;
            move.pos = Pos;
            move.bossName.text = bossName;
        };
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
