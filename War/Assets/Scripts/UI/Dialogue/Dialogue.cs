using UnityEngine;

public class Dialogue : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void OnEnable()
    {
        ((RectTransform)transform).anchoredPosition = Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
