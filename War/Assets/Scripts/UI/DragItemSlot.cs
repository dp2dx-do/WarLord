using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItemSlot : MonoBehaviour, IDropHandler
{
    public int index;
    public DragItem drItem;
    
    public void OnDrop(PointerEventData eventData)
    {
        DragItem dragItem = eventData.pointerDrag.GetComponent<DragItem>();
        Inventory.Instance.ChangePlayerData(dragItem.orgParent.GetComponent<DragItemSlot>().index, index);
        if (drItem != null)
        {
            drItem.transform.SetParent(dragItem.orgParent);
            drItem.index = dragItem.orgParent.GetComponent<DragItemSlot>().index;
            drItem.transform.localPosition = Vector3.zero;
        }
        else
        {
            dragItem.orgParent.GetComponent<DragItemSlot>().drItem = null;
        }
        dragItem.ChangeParent(transform);
        drItem = dragItem;
        drItem.index = index;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        drItem = GetComponentInChildren<DragItem>();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
