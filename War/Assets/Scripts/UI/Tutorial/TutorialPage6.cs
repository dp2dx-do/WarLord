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
                text.text = "����Ʈ�� �Ϸ��ߴ�. ��Ż�� Ŭ���Ͽ� ������ ���ư���.";
                portal.gameObject.SetActive(true);
                portal.tutorial += () =>
                {
                    text.gameObject.SetActive(false);
                };
            }
        };
        
    }
    
}
