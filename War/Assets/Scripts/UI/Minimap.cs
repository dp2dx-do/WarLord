using UnityEngine;

public class Minimap : MonoBehaviour
{
    public static Minimap Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        DontDestroyOnLoad(transform.parent.gameObject);
    }

    // Update is called once per frame
    
}
