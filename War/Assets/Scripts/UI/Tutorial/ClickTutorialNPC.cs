
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickTutorialNPC : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == 0)
        {
            GetComponent<Image>().raycastTarget = false;
            transform.parent.GetComponent<Image>().raycastTarget = false;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(eventData.position), out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Npc")))
            {
                NPCScript npc = hit.transform.GetComponent<NPCScript>();
                PlayerScript player = FindFirstObjectByType<PlayerScript>();
                npc.PopUp.gameObject.SetActive(true);
                player.IsTalk = true;
                player.transform.position = npc.transform.forward * 2f + npc.transform.position;
                player.transform.LookAt(npc.transform.position);
                npc.GetComponent<TalkNPC>().Page = 0;
                BasicCanvas.Instance.Dialogue = npc.PopUp.gameObject;
            }
            ++TutorialManager.Instance.Page;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
