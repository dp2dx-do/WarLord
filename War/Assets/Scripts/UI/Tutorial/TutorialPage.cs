using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialPage : MonoBehaviour, IPointerClickHandler
{
    int index = 0;
    [SerializeField] GameObject[] gameObjects;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == 0 && index < gameObjects.Length)
        {
            gameObjects[index++].SetActive(true);
        }
    }
    void Start()
    {
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(false);
        }
    }
}
