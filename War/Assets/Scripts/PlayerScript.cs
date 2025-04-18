using System.Collections;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEditor.UIElements;
using System.Linq;
using UnityEngine.EventSystems;
using Unity.VisualScripting;


public struct ComponentProptery<T>
{
    T _inst;
    public T this[MonoBehaviour comp]
    {
        get
        {
            if (_inst == null)
            {
                _inst = comp.GetComponentInChildren<T>();
            }
            return _inst;
        }
    }
}
public enum CharClass {Knight, Mage}
[Serializable]
public class PlayerData : IComparable<PlayerData>
{
    public CharClass charClass;
    public int id;
    public string charName;
    public long HP;
    public int MP;
    public int Level, Exp, Gold;
    public EquipItem[] inventoryEquipItems;
    public UseItem[] inventoryUseItems;
    public EquipItem[] equipItems;
    public int[] usingItem;
    public int[] SkillLevels;
    public List<Quest> quests;
    public bool isTutorialOpen = true;
    public PlayerData(int index)
    {
        id = index;
        HP = 710;
        MP = 1020;
        Level = 1;
        Gold = 100;
        equipItems = new EquipItem[5];
        usingItem = new int[4];
        inventoryEquipItems = new EquipItem[GameData.Instance.inventoryItemLength];
        inventoryUseItems = new UseItem[GameData.Instance.inventoryItemLength];
        SkillLevels = new int[7];
        isTutorialOpen = true;
    }

    public int CompareTo(PlayerData other)
    {
        return id.CompareTo(other.id);
    }
}

public class PlayerScript : BattleData
{
    public int char_id;
    public string charName;
    public CharClass charClass;
    public static System.Random rand;
    public event UnityAction changeMP;
    public Vector3 navMeshOffset;
    public long STR { get;private set; }
    public long StatATK { get; private set; }
    int _mp;
    public int MP { get =>_mp; 
        set
        {
            _mp = value;
            changeMP?.Invoke();
        }
    }
    int _exp;
    public event UnityAction changeEXP;
    public int EXP
    {
        get => _exp;
        set
        {
            _exp = value;
            if(_exp >= MaxExp && Level < 100)
            {
                _exp -= MaxExp;
                Level++;
            }
            if(Level >= 100)
            {
                _exp = 0;
            }
            changeEXP?.Invoke();
        }
    }
    int _level = 1;
    public event UnityAction changeLV;
    public int Level
    {
        get => _level;
        set
        {
            _level = value;
            changeLV?.Invoke();
        }
    }
    public event UnityAction changeGold;
    int _gold;
    public int Gold
    {
        get => _gold;
        set
        {
            _gold = value;
            changeGold?.Invoke();
        }
    }
    public int Defense { get; private set; }
    public int MaxMP { get; private set; }
    public int MaxExp { get; private set; }
    public bool Live;
    private float delta;
    [HideInInspector] public float delay;
    [HideInInspector] public float[] CoolTimes, OriginalCoolTime;
    public int[] SkillLevels;
    [HideInInspector] public int SkillPoint, UsedSkillPoint;
    public Rigidbody rigid;
    public Skills[] useSkills;    
    KeyCode[] skillKeyCode;
    KeyCode[] useKeyCode;
    public Vector3 Destination;
    public bool IsTalk;
    public List<Quest> quests;
    public UseItem[] inventoryUseItems;
    public EquipItem[] inventoryEquipItems;
    public EquipItem[] equipItems;
    public UsingItem usingItem;
    public bool isTutorialOpen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rand = new System.Random();
        DontDestroyOnLoad(gameObject);
        base.Start();
        MaxExp = 100;
        skillKeyCode = new KeyCode[] {
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R,
        KeyCode.A, KeyCode.S, KeyCode.D
        };
        useKeyCode = new KeyCode[] {
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4
        };
        delta = 0f;
        OriginalCoolTime = new float[useSkills.Length];
        for (int i = 0; i < OriginalCoolTime.Length; i++)
        {
            OriginalCoolTime[i] = useSkills[i].CoolTime;
        }
        changeEXP += () => SliderValueChange.instance.EXPChange();
        changeGold += () => TimeAndGold.Instance.GoldChange(_gold);
        changeLV += () =>
        {
            PlayerStatChange();
            SliderValueChange.instance.LevelChange();
        };
        changeHP += () => SliderValueChange.instance.HPChange();
        changeMP += () => SliderValueChange.instance.MPChange();
        CoolTimes = new float[useSkills.Length];


        charName = GameData.Instance.playerData.charName;
        char_id = GameData.Instance.playerData.id;
        SkillLevels = GameData.Instance.playerData.SkillLevels;
        Gold = GameData.Instance.playerData.Gold;
        Level = GameData.Instance.playerData.Level;
        EXP = GameData.Instance.playerData.Exp;
        HP = GameData.Instance.playerData.HP;
        MP = GameData.Instance.playerData.MP;
        if (GameData.Instance.playerData.quests != null)
        {
            quests = GameData.Instance.playerData.quests.ToList();
        }
        if (quests == null) quests = new List<Quest>();
        foreach(QuestSO q in QuestManager.instance.AllQuest.Values)
        {
            if (!quests.Exists(item => item.QuestID.Equals(q.questID)))
            {
                quests.Add(new Quest(q.questID));
            }
        }
        isTutorialOpen = GameData.Instance.playerData.isTutorialOpen;
        inventoryUseItems = new UseItem[GameData.Instance.inventoryItemLength];
        GameData.Instance.playerData.inventoryUseItems.CopyTo(inventoryUseItems, 0);
        inventoryEquipItems = new EquipItem[GameData.Instance.inventoryItemLength];
        GameData.Instance.playerData.inventoryEquipItems.CopyTo(inventoryEquipItems, 0);
        equipItems = new EquipItem[5];
        GameData.Instance.playerData.equipItems.CopyTo(equipItems, 0);
        if (equipItems[0] == null || equipItems[0].ItemID == 0)
        {
            equipItems[0] = new EquipItem(10);
        }

        usingItem = FindFirstObjectByType<UsingItem>();
        for(int i=0; i<4; i++)
        {
            if(GameData.Instance.playerData.usingItem[i] != 0)
            {
                usingItem.slots[i].UsingItem = Array.Find(inventoryUseItems, x => x.ItemID == GameData.Instance.playerData.usingItem[i]);
                usingItem.slots[i].ChangeIconText();
            }
        }
        Live = true;
        navMeshOffset = Vector3.up * 0.05f;
        rigid = GetComponent<Rigidbody>();
        PlayerStatChange();
        QuestSlot2.Instance.Change();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (delay < 0f && Live)
        {
            if (!IsTalk)
            {

                if (Input.GetMouseButtonDown(1))
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
                    {
                        if (!EventSystem.current.IsPointerOverGameObject())
                        {
                            MoveMent(hit.point);
                        }
                    }
                }
                if (Input.GetMouseButtonDown(0) && (BasicCanvas.Instance.Dialogue == null || !BasicCanvas.Instance.Dialogue.activeSelf))
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Npc")))
                    {
                        if (!EventSystem.current.IsPointerOverGameObject())
                        {
                            TalkWithNPC(3f, hit);
                        }
                    }
                }
            
                if (!ChatWindow.instance.inputField.isFocused)
                {
                    for (int i = 0; i < skillKeyCode.Length; i++)
                    {
                        if (Input.GetKeyDown(skillKeyCode[i]) && CoolTimes[i] < 0f && MP >= useSkills[i].UseMP)
                        {
                            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
                            {
                                transform.LookAt(hit.point);
                            }
                            myAnim.SetBool("IsRun", false);
                            StopMove();
                            Skills s = Instantiate(useSkills[i], transform.position, transform.rotation);
                            s.usedPlayer = this;
                            s.Damage = (long)(StatATK * (s.Multipler * (1 + 0.1 * SkillLevels[i])) * (rand.NextDouble() * 0.1 + 0.9));
                            delay = s.Delay;
                            MP -= s.UseMP;
                            CoolTimes[i] = s.CoolTime;
                        }
                    }
                    for(int i=0; i<useKeyCode.Length; i++)
                    {
                        if (Input.GetKeyDown(useKeyCode[i]) && usingItem.slots[i].UsingItem!=null)
                        {
                            usingItem.slots[i].UsingItem.ItemCount--;
                            UseItemData use = (UseItemData)GameData.Instance.AllItem[usingItem.slots[i].UsingItem.ItemID];
                            HP += use.Recovery + (long)(MaxHP * use.RecoveryPercent);
                            HP = Math.Min(HP, MaxHP);
                            if (usingItem.slots[i].UsingItem.ItemCount < 1)
                            {
                                usingItem.slots[i].UsingItem.ItemID = 0;
                                usingItem.slots[i].UsingItem = null;
                            }
                            usingItem.slots[i].ChangeIconText();

                        }
                    }
                }
            }
        }
        if (Live)
        {
            delay -= Time.deltaTime;

            for (int i = 0; i < CoolTimes.Length; i++)
            {
                if (CoolTimes[i] >= 0f)
                {
                    CoolTimes[i] -= Time.deltaTime;
                }
            }
            delta += Time.deltaTime;
            if (delta > 1)
            {
                delta -= 1;
                HP = Math.Min(HP + MaxHP / 100, MaxHP);
                MP = Mathf.Min(MP + 85 + (int)(Mathf.Sqrt(Level) * 2), MaxMP);
            }
        }
    }

    public void MoveMent(Vector3 dest)
    {

        Destination = dest + navMeshOffset;
        StopMove();
        MoveCo = StartCoroutine(Run(Destination, moveSpeed));
    }
    public void TalkWithNPC(float dist, RaycastHit hit)
    {
        NPCScript npc = hit.transform.GetComponent<NPCScript>();
        if (Vector3.Distance(npc.transform.position, transform.position) < dist)
        {
            npc.action?.Invoke();
            if (npc.GetComponent<TalkNPC>() != null)
            {
                IsTalk = true;
                transform.position = npc.transform.forward * 2f + npc.transform.position;
                transform.LookAt(npc.transform.position);
                npc.GetComponent<TalkNPC>().Page = 0;
            }
            StopMove();
            myAnim.SetBool("IsRun", false);
            Fading.Instance.BeforePos = transform.position;
            BasicCanvas.Instance.Dialogue = npc.PopUp.gameObject;
        }
    }

    public void PlayerStatChange()
    {
        MaxHP = 700 + 10 * Level;
        MaxMP = 1000 + 20 * Level;
        Defense = 0;
        STR = 50 + 5 * Level;
        MaxExp = 30 + Level * Level * 3;
        SkillPoint = Level * (Level + 3) / 2;
        UsedSkillPoint = 0;
        for (int i = 0; i < SkillLevels.Length; i++)
        {
            for (int j = 0; j < SkillLevels[i]; j++)
            {
                UsedSkillPoint += GameData.UsingSkillPoints[j];
            }
        }
        AttackPower = 0;
        for (int i = 0; i < equipItems.Length; i++)
        {
            if (equipItems[i] != null)
            {
                if (equipItems[i].ItemID != 0)
                {
                    AttackPower += equipItems[i].AttackPower;
                    Defense += equipItems[i].Defense;
                    STR += equipItems[i].STR;
                    MaxHP += equipItems[i].HP;
                }
            }
        }
        MaxHP = (long)Math.Ceiling(MaxHP * (1 + Defense * 0.002));
        StatATK = AttackPower * STR / 25;
    }

    public IEnumerator Run(Vector3 dest, float speed)
    {
        myAnim.SetBool("IsRun", true);
        yield return MoveCo2 = StartCoroutine(PathRun(dest, speed));
        myAnim.SetBool("IsRun", false);
        MoveCo = null;
    }
    void LateUpdate()
    {
        Camera.allCameras[1].transform.position = new Vector3(transform.position.x, 12f, transform.position.z);
        if (IsTalk)
        {
            Vector3 moveTo = transform.position + transform.forward;
            moveTo.y = 0;

            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, Quaternion.Euler(45, transform.rotation.eulerAngles.y+90f, 0), Time.deltaTime * 2f);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, moveTo + Camera.main.transform.forward * -4f, Time.deltaTime * 5f);
        }
        else
        {
            Vector3 moveTo = transform.position;
            moveTo.y = 0;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, moveTo + new Vector3(0, 9f, -6f), Time.deltaTime * 5f);
            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, Quaternion.Euler(55, 0, 0), Time.deltaTime * 5f);

        }
    }
    public void SaveTheData()
    {
        if (TutorialManager.Instance != null) return;
        Debug.Log("Save");
        PlayerData pd = new PlayerData(char_id);
        pd.charClass = charClass;
        pd.charName = charName;
        pd.HP = HP;
        pd.MP = MP;
        pd.Exp = EXP;
        pd.Gold = Gold;
        pd.Level = Level;
        pd.SkillLevels = SkillLevels;
        pd.inventoryUseItems = inventoryUseItems;
        pd.inventoryEquipItems = inventoryEquipItems;
        for(int i=0; i<4; i++)
        {
            pd.usingItem[i] = usingItem.slots[i].UsingItem.ItemID;
        }
        pd.equipItems = equipItems;
        pd.isTutorialOpen = isTutorialOpen;
        pd.quests = quests.ToList();
        GameData.Instance.playerData = pd;
    }
    private void OnApplicationQuit()
    {
        SaveTheData();
    }

    public override void HitScript(long dmg)
    {
        if(HP > 0)
        {
            HP -= dmg;
            myAnim.SetTrigger("IsHit");
            TextMeshProUGUI txt = Instantiate(Resources.Load<TextMeshProUGUI>("DmgText"), 
                Camera.main.WorldToScreenPoint(DamagePos.position), Quaternion.identity);
            txt.text = dmg.ToString("N0");
            txt.color = Color.red;
            if (HP <= 0)
            {
                HP = 0;
                DieScript();
            }
        }
    }
    public override void DieScript()
    {
        Live = false;
        myAnim.SetTrigger("IsDie");
        if (BossMap.instance != null)
        {
            BossMap.instance.DiePlayer(this);
        }
        else
        {
            StartCoroutine(GoToTown());
        }
    }

    IEnumerator GoToTown()
    {
        yield return new WaitForSeconds(3f);
        Fading.Instance.Fade(2);
        Fading.Instance.pos = Vector3.zero;
        Revive();
    }
    public void Revive()
    {
        Live = true;
        HP = MaxHP;
    }
}
