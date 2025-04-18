using UnityEngine;

public class TutorialPage8 : MonoBehaviour
{
    public GameObject raycast;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        QuestUI qui = FindFirstObjectByType<QuestUI>();
        qui.Page = 1;
        qui.OnEnable();
        FindFirstObjectByType<QuestSlot>().action += () => raycast.SetActive(false);
        qui.closed = () =>
        {
            TutorialManager.Instance.Page++;
            qui.closed = null;
        };
    }
}
