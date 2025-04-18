using UnityEngine;


[System.Serializable]
public class Buff {
    public float LastTime;
    public int Stat;
    public long Amount;
    public double Multipler;
}

public class UseBuff : Skills
{
    public Buff buff;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    


    // Update is called once per frame
    void Update()
    {
        
    }
}
