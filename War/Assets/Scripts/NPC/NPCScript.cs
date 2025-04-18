using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NPCScript : MonoBehaviour
{
    public GameObject PopUp;
    public Sprite MinimapIconSprite;
    MinimapIcon icon;
    public UnityAction action;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        icon = Instantiate(Resources.Load<MinimapIcon>("UI/IconMinimap"), Minimap.Instance.transform);
        icon.Target = transform;
        icon.Set(transform, MinimapIconSprite);
        action += () =>
        {
            if (PopUp != null)
            {
                PopUp.SetActive(true);
            }
        };
    }

    public virtual void SetDialogue(int page)
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnDestroy()
    {
        if (icon != null)
        {
            Destroy(icon.gameObject);
        }
    }
}
