using UnityEngine;
using UnityEngine.UI;

public class ChaosDungeonDialogue : Dialogue
{
    [SerializeField] Transform Diff;
    [SerializeField] Button[] buttons;
    public int[] MapTo;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        buttons = Diff.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[index].onClick.AddListener(delegate
            {
                Fading.Instance.Fade(MapTo[index]);
                Fading.Instance.pos = Vector3.zero;
                gameObject.SetActive(false);
            });
        }
        gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
