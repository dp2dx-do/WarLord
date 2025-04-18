using UnityEngine;
using UnityEngine.UI;

public class MinimapIcon : MonoBehaviour
{
    public Transform Target;
    public Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Target != null && Camera.allCamerasCount >1)
        {
            Vector3 pos= Camera.allCameras[1].WorldToViewportPoint(Target.position);
            Vector2 size = transform.parent.GetComponent<RectTransform>().sizeDelta;
            pos.x *= size.x;
            pos.y *= size.y;
            pos.z = 0;
            ((RectTransform)transform).anchoredPosition = pos;
        }
        if (Target == null || !Target.gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    public void Set(Transform target, Sprite icon)
    {
        Target = target;
        image.sprite = icon;
    }
}
