using UnityEngine;
using UnityEngine.UI;

public class DeleteCharWarning : MonoBehaviour
{
    public int index;
    [SerializeField] Button yesBtn, noBtn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        yesBtn.onClick.AddListener(delegate
        {
            GameData.Instance.save.playerDatas.Remove(GameData.Instance.save.playerDatas[index]);
            Fading.Instance.Fade(10);
        });
        noBtn.onClick.AddListener(delegate { gameObject.SetActive(false); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
