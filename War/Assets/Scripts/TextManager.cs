using UnityEngine;
using System.IO;
using Unity.Collections;

[System.Serializable]
public class NPCSays 
{
    public int page;

    public string[] content;
}



public class TextManager : MonoBehaviour
{
    public static TextManager instance;
    public NPCSays NPCSays { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        if (File.Exists(Application.persistentDataPath + "/TextManager.json"))
        {
            string fileText = File.ReadAllText(Application.persistentDataPath + "/TextManager.json");
            
        }
        else
        {
            
        }
    }

    private void OnApplicationQuit()
    {
        string txt = JsonUtility.ToJson(NPCSays);
        File.WriteAllText(Application.persistentDataPath + "/TextManager.json", txt);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
