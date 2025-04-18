using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSystem : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI HoneyTip;
    private string[] HoneyTips;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Loading());
        HoneyTips = new string[]
        {
            "새로운 장비를 착용하지 않고 기존 장비를 착용 해제할 수 없다.",
            "장비 아이템은 인벤토리에서 더블클릭을 해야 착용할 수 있다.",
            "소비 아이템은 인벤토리에서 드래그 앤 드롭해서 1~4 키를 눌러야 사용할 수 있다.",
            "스킬 레벨을 1씩 올리면 데미지가 1.1배로 증가한다.\n증가하는 데미지는 합적용된다.",
            "키 세팅 변경은 지원하지 않을 예정이다."          
        };
        HoneyTip.text = HoneyTips[Random.Range(0, HoneyTips.Length)];
    }
    public IEnumerator Loading()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(Fading.Instance.TargetScene);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            slider.value = asyncOperation.progress / 0.9f;
            yield return null;
            if (asyncOperation.progress >= 0.9f)
            {
                slider.value = 1f;
                yield return new WaitForSeconds(1f);
                asyncOperation.allowSceneActivation = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
