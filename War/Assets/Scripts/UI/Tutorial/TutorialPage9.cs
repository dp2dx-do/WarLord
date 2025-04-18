using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialPage9 : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        TutorialManager.Instance.Page++;
    }

}
