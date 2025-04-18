using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialPage5 : MonoBehaviour, IPointerClickHandler
{
    PlayerScript player;
    [SerializeField] GameObject img, text;
    ChaosDungeonNPC npc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<PlayerScript>();
        npc = FindFirstObjectByType<ChaosDungeonNPC>();
    }

    void Update()
    {
        if (npc != null)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(npc.transform.position);
            pos.x = Mathf.Clamp(pos.x, 80, Screen.width - 80);
            pos.y = Mathf.Clamp(pos.y, 80, Screen.height - 80);
            img.transform.position = pos;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                player.MoveMent(hit.point);
            }
        }
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Npc")))
            {
                if (hit.transform.GetComponent<ChaosDungeonNPC>()!=null && Vector3.Distance(hit.transform.position, player.transform.position) < 2.5f)
                {
                    GetComponent<Image>().color = Color.clear;
                    img.gameObject.SetActive(false);
                    text.gameObject.SetActive(false);
                    Fading.Instance.BeforePos = player.transform.position;
                    Fading.Instance.Fade(5);

                }
            }
        }
    }
}
