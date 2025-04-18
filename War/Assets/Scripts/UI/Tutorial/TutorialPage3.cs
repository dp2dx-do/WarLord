using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialPage3 : MonoBehaviour, IPointerClickHandler
{
    PlayerScript player;
    [SerializeField] GameObject img;
    QuestNPC npc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        npc = FindFirstObjectByType<QuestNPC>();
    }

    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(npc.transform.position);
        pos.x = Mathf.Clamp(pos.x, 80, Screen.width - 80);
        pos.y = Mathf.Clamp(pos.y, 80, Screen.height - 80);
        img.transform.position = pos;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(player == null) player = FindFirstObjectByType<PlayerScript>();
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                player.MoveMent(hit.point);
            }
        }
        if(eventData.button ==PointerEventData.InputButton.Left)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Npc")))
            {
                if (hit.transform.GetComponent<QuestNPC>()!=null & Vector3.Distance(hit.transform.position, player.transform.position) < 2.5f)
                {
                    player.TalkWithNPC(3f, hit);
                    ++TutorialManager.Instance.Page;
                }
            }
        }
    }
}
