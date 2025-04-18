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
            "���ο� ��� �������� �ʰ� ���� ��� ���� ������ �� ����.",
            "��� �������� �κ��丮���� ����Ŭ���� �ؾ� ������ �� �ִ�.",
            "�Һ� �������� �κ��丮���� �巡�� �� ����ؼ� 1~4 Ű�� ������ ����� �� �ִ�.",
            "��ų ������ 1�� �ø��� �������� 1.1��� �����Ѵ�.\n�����ϴ� �������� ������ȴ�.",
            "Ű ���� ������ �������� ���� �����̴�."          
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
