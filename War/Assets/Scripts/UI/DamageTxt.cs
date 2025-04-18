using TMPro;
using UnityEngine;

public class DamageTxt : MonoBehaviour
{
    TextMeshProUGUI txt;
    public Color origin;
    float timeT;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.SetParent(SceneData.instance.uiCanvas.transform);
        txt = GetComponent<TextMeshProUGUI>();
        origin = txt.color;
        Destroy(gameObject, 2f);
        timeT = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeT += Time.deltaTime;
        transform.Translate(Vector3.up * Time.deltaTime * 50f);
        origin.a = Mathf.Lerp(1, 0, timeT / 2f);
        txt.color = origin;
    }
}
