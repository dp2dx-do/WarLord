using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialSkip : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == 0)
        {
            TutorialManager.Instance.Page = 99;
        }
    }
}
