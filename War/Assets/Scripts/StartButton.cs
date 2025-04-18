using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Button btn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btn.onClick.AddListener(delegate
        {
            Fading.Instance.Fade(10);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
