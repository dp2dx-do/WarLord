using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDialogue : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector2 clickedPos;
    public void OnBeginDrag(PointerEventData eventData)
    {
        
            clickedPos = eventData.position - (Vector2)transform.parent.position;

    }

    public void OnDrag(PointerEventData eventData)
    {

        Vector3 pos = eventData.position - clickedPos;
        Vector3 size = transform.parent.GetComponent<RectTransform>().sizeDelta;
        pos.x = Mathf.Clamp(pos.x, size.x / 2, Screen.width - size.x / 2);
        pos.y = Mathf.Clamp(pos.y, size.y / 2, Screen.height - size.y / 2);
        transform.parent.position = pos;

    }

    public void OnEndDrag(PointerEventData eventData)
    {

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
