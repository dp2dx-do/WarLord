using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] Slider Bgm, Effect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Bgm.value = SoundManager.instance.Bgm.volume;
        Effect.value = SoundManager.instance.Effect.volume;
        Bgm.onValueChanged.AddListener(delegate (float f)
        {
            SoundManager.instance.BgmVolume = f;
        });
        Effect.onValueChanged.AddListener(delegate (float f)
        {
            SoundManager.instance.EffectVolume = f;
        });
    }
}
