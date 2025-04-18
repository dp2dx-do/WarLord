using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreateChar : MonoBehaviour
{
    public CharClass charClass;
    public TMP_Dropdown dropdown;
    public TMP_InputField inputField;
    public Button createBtn;
    [SerializeField] PreviewChar[] previewChars;
    [SerializeField] Transform moveToPos;
    [SerializeField] GameObject warning;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        dropdown.onValueChanged.AddListener(delegate(int i)
        {
            previewChars[(int)charClass].Move(previewChars[(int)charClass].orgPos);
            charClass = (CharClass)i;
            previewChars[i].Move(moveToPos);
        });
        createBtn.onClick.AddListener(delegate
        {
            if(inputField.text.Length > 1 && inputField.text.Length < 13 && !inputField.text.Contains(" "))
            {
                int i = 0;
                while (GameData.Instance.save.playerDatas.Exists(x => x.id == i))
                {
                    ++i;
                }
                PlayerData data = new PlayerData(i);
                data.charClass = charClass;
                data.charName = inputField.text;
                GameData.Instance.save.playerDatas.Add(data);
                Fading.Instance.Fade(10);
            }
            else
            {
                warning.SetActive(true);
            }
        });
    }

    private void OnEnable()
    {
        previewChars[(int)charClass].Move(moveToPos);
    }

    private void OnDisable()
    {
        if (previewChars != null)
        {
            foreach (PreviewChar preview in previewChars)
            {
                if (preview != null && Camera.allCameras.Length > 1)
                {
                    preview.transform.position = preview.orgPos.position;
                    preview.transform.LookAt(Camera.allCameras[1].transform);
                }
            }
        }
    }
}
