using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    public static SoundManager instance;
    public AudioSource Bgm, Effect;
    float _bgm, _effect;
    public float BgmVolume
    {
        get
        {
            return _bgm;
        }
        set
        {
            _bgm = Mathf.Clamp01(value);
            Bgm.volume = _bgm;
        }
    }
    public float EffectVolume
    {
        get
        {
            return _effect;
        }
        set
        {
            _effect = Mathf.Clamp01(value);
            Effect.volume = _effect;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        BgmVolume = 0.5f;
        EffectVolume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
