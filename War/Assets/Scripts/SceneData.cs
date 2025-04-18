using UnityEngine;

public class SceneData : MonoBehaviour
{
    public static SceneData instance;
    public Canvas uiCanvas;
    public AudioClip BgmClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        
        // Update is called once per frame
    }
       
}
