using UnityEngine;
using UnityEngine.UI;

public class VolumeUI : MonoBehaviour
{
    [SerializeField] Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = SoundManager.instance.Bgm.volume;
        slider.onValueChanged.AddListener(delegate(float f)
        {
            SoundManager.instance.BgmVolume = f;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
