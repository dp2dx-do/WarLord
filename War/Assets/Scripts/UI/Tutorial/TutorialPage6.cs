using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TutorialPage6 : MonoBehaviour
{
    public UnityAction action;
    public TextMeshProUGUI text;
    public TownPortal portal;
    public int kills;
    void Start()
    {
        portal = FindFirstObjectByType<TownPortal>();
        portal.gameObject.SetActive(false);
        action = () =>
        {
            if (++kills == 5)
            {
                NormalMonster[] nms = FindObjectsByType<NormalMonster>(FindObjectsSortMode.None);
                foreach (NormalMonster n in nms) { Destroy(n.gameObject); }
                foreach (Spawner s in FindObjectsByType<Spawner>(FindObjectsSortMode.None)) { Destroy(s.gameObject); }
                text.text = "퀘스트를 완료했다. 포탈을 클릭하여 마을로 돌아가자.";
                portal.gameObject.SetActive(true);
                portal.tutorial += () =>
                {
                    text.gameObject.SetActive(false);
                };
            }
        };
        
    }
    
}
