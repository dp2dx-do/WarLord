using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChatWindow : MonoBehaviour
{
    public static ChatWindow instance;
    Color[] textColor;
    int colorNum;
    public Transform content;
    public TMPro.TMP_InputField inputField;
    public Scrollbar scrollbar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;   
        colorNum = 0;
        textColor = new Color[] { Color.white, new Color(1f, 1f, 0f), Color.cyan, Color.magenta };
        
        inputField.onSubmit.AddListener(delegate
        {
            StartCoroutine(SubMit());
        });
    }
    IEnumerator SubMit()
    {
        inputField.DeactivateInputField();
        yield return null;
        Debug.Log(inputField.text.Length);
        if (inputField.text != string.Empty)
        {
            inputField.selectionFocusPosition = 0;
            ChatItem chat = content.GetComponentInChildren<ChatItem>();
            chat.transform.SetAsLastSibling();
            chat.text_.text = inputField.text;
            chat.text_.color = textColor[colorNum];
            inputField.text = string.Empty;
            inputField.ActivateInputField();
            scrollbar.value = 0f;
            inputField.caretPosition = 0;
        }
    }
    public void ChangeColor(int n)
    {
        colorNum = n;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
