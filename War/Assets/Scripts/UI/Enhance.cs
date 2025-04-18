using System.Collections;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Successrates 
{
    public float[] rates;
}


public class Enhance : MonoBehaviour, IDropHandler
{
    public static float[] SuccessRate = {100f, 95f, 90f, 85f, 80f, 75f, 70f, 65f, 60f, 60f,
        55f, 55f, 50f, 50f, 45f, 45f, 40f, 40f, 35f, 35f,
    30f, 30f, 6f, 4f, 2f};
    
    public static Enhance instance;
    public EquipItem currentEquipItem;
    public Image itemImg;
    public GameObject resultBack, resultWindow;
    public TextMeshProUGUI infoText, goldText, resultText;
    public Button EnhanceBtn;
    private PlayerScript player;
    public void OnDrop(PointerEventData eventData)
    {
        DragItem dragItem = eventData.pointerDrag.GetComponent<DragItem>();
        if (dragItem != null)
        {
            if(dragItem.Item.type == Item.Type.Equip)
            {
                PlayerScript player = FindFirstObjectByType<PlayerScript>();
                currentEquipItem = player.inventoryEquipItems[dragItem.index];
                if (currentEquipItem != null)
                {
                    TextUpdate();
                    itemImg.sprite = GameData.Instance.AllItem[currentEquipItem.ItemID].icon;
                }
            }
        }
    }

    void TextUpdate()
    {
        if (currentEquipItem != null)
        {
            if(currentEquipItem.Star < Mathf.Min( currentEquipItem.MaxStar, SuccessRate.Length))
            {
                infoText.text =
                    currentEquipItem.Star + "�� -> " + (currentEquipItem.Star + 1).ToString() + "��\n����Ȯ�� : " 
                    + SuccessRate[currentEquipItem.Star].ToString("F2") + "%"
                    + "\n����Ȯ�� : " + ((100f - SuccessRate[currentEquipItem.Star]) * (1f - currentEquipItem.Star * 0.011f)).ToString("F2") + "%"
                    + "\n�϶�Ȯ�� : " + ((100f - SuccessRate[currentEquipItem.Star]) * (currentEquipItem.Star * 0.011f)).ToString("F2") + "%";
                goldText.text = "�ʿ��� ��� : " + RequireGold().ToString("N0");


            }
            else
            {
                infoText.text = "�ִ�ġ.";
                goldText.text = "�ִ�ġ.";

            }
        }
        else 
        {   infoText.text = "��ȭ�� ��� �÷��ּ���.\n�� �������� �Ѱ�� ���� ���� �������� �ø� �� �����ϴ�.";
            goldText.text = "";
        }
    }

    IEnumerator EnhanceItem()
    {
        if (currentEquipItem.Star < SuccessRate.Length)
        {
            if (player != null)
            {
                if (player.Gold >= RequireGold())
                {
                    resultBack.SetActive(true);
                    resultWindow.SetActive(false);
                    yield return new WaitForSeconds(0.5f);
                    resultWindow.SetActive(true);
                    player.Gold -= RequireGold();
                    if (SuccessRate[currentEquipItem.Star] > Random.Range(0f, 100f))
                    {
                        ++currentEquipItem.Star;
                        resultText.text = "��ȭ�� �����Ͽ����ϴ�.";
                        resultText.color = Color.yellow;

                    }
                    else if (Random.Range(0f, 100f) < currentEquipItem.Star * 1.1f)
                    {
                        --currentEquipItem.Star;
                        resultText.text = "��ȭ�� �����Ͽ� ��ȭ �ܰ谡 �϶��Ͽ����ϴ�.";
                        resultText.color = Color.black;
                    }
                    else
                    {
                        resultText.text = "��ȭ�� �����Ͽ����ϴ�.";
                        resultText.color = Color.red;
                    }
                    TextUpdate();
                }
            }
        }
    }


     
    int RequireGold()
    {
        return Mathf.CeilToInt
            (2.5f * Mathf.Pow(currentEquipItem.Star + 1, 1.4f)
            * Mathf.Pow(((EquipItemData)GameData.Instance.AllItem[currentEquipItem.ItemID]).itemClass + 1, 1.9f));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (File.Exists(Application.dataPath + "/SuccessRate.json"))
        {
            Successrates s = JsonUtility.FromJson<Successrates>(File.ReadAllText(Application.dataPath + "/SuccessRate.json"));
            SuccessRate = s.rates;
        }
        else
        {
            Successrates s = new Successrates();
            s.rates = SuccessRate;
            File.WriteAllText(Application.dataPath + "/SuccessRate.json", JsonUtility.ToJson(s));
        }
        
        EnhanceBtn.onClick.AddListener(delegate 
        {
            StartCoroutine(EnhanceItem());
        });
    }

    void OnEnable()
    {
        player = FindFirstObjectByType<PlayerScript>();
        if (Inventory.Instance == null || !Inventory.Instance.isActiveAndEnabled)
        {
            BasicCanvas.Instance.BotRightButtonOpenObjects[2].SetActive(true);
        }
        BasicCanvas.Instance.BotRightButtonOpenObjects[2].transform.position = new Vector3(Screen.width * 0.75f, Screen.height * 0.5f);
       
        currentEquipItem = null;
        itemImg.sprite = null;
        TextUpdate();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
