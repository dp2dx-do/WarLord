using UnityEngine;
using UnityEngine.Events;

public class TutorialPage4 : MonoBehaviour
{
    public GameObject raycast;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QuestUI qui = FindFirstObjectByType<QuestUI>();
        qui.OnEnable();
        FindFirstObjectByType<QuestSlot>().action += () => raycast.SetActive(false);

        qui.closed = () =>
        {
            if (TutorialManager.Instance.Page == 3)
            {
                TutorialManager.Instance.Page++;
            }

        };
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
