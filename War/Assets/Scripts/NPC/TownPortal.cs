using UnityEngine;
using UnityEngine.Events;

public class TownPortal : NPCScript
{
    public UnityAction tutorial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
        action += () =>
        {
            tutorial?.Invoke();
            Fading.Instance.pos = Fading.Instance.BeforePos;
            Fading.Instance.Fade(2);
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
