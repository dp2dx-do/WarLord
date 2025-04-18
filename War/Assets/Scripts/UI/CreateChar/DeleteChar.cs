using UnityEngine;
using UnityEngine.EventSystems;

public class DeleteChar : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    public int index;
    int clickCount;
    [SerializeField] DeleteCharWarning warning;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(++clickCount > 1)
        {
            warning.gameObject.SetActive(true);
            warning.index = index;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        clickCount = 0;
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
