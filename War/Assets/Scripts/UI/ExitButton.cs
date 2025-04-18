using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public Button Exit { get; private set; }
    public event UnityAction action;
    [SerializeField] Vector3 pos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Exit = GetComponent<Button>();
        Exit.onClick.AddListener(delegate
        {
            Fading.Instance.Fade(2);
            Fading.Instance.pos = Fading.Instance.BeforePos;
            action?.Invoke();
        });
        if (TutorialManager.Instance != null)
        {
            Exit.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
