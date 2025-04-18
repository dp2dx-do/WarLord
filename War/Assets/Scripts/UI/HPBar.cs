using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Transform monster;
    public Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.SetParent(SceneData.instance.uiCanvas.transform);
        slider = GetComponent<Slider>();
        slider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
