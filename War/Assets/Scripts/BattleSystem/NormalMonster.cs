using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class NormalMonster : Monster
{
    [SerializeField] int MaxHPSetting, AttackPowerSetting;
    public HPBar HP_Bar;
    [SerializeField] int giveExp, giveGold;
    public float attackSpeed, attackRange;
    public float attackMotionTime;
    public Transform HPBarPos;
    public Spawner Spawned;
    [SerializeField] ItemData[] DropTable;
    public UnityAction<int> action;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        state = States.Normal;
        delay = 1f;
        MaxHP = MaxHPSetting;
        AttackPower = AttackPowerSetting;
        if (HPBarPos != null)
        {
            if (HP_Bar == null)
            {
                HP_Bar = Instantiate(Resources.Load<HPBar>("Prefabs/HPBar"), HPBarPos.position, Quaternion.identity);
            }
            else
            {
                HP_Bar.gameObject.SetActive(true);
                HP_Bar.slider.value = 1f;
            }
        }
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP_Bar != null)
        {
            HP_Bar.transform.position = Camera.main.WorldToScreenPoint(DamagePos.transform.position);
        }
        if (HP > 0)
        {
            if (state == States.Battle)
            {
                if (Vector3.Distance(transform.position, target.transform.position) > attackRange)
                {
                    Follow();
                }
                if (delay < 0 && Vector3.Distance(target.transform.position, transform.position) < attackRange)
                {
                    delay = 1f / attackSpeed;
                    myAnim.SetTrigger("OnAttack");
                    StartCoroutine(AttackScript());
                }
            }
            else if (state == States.Normal && MoveCo == null)
            {
                MoveCo = StartCoroutine(Move());
            }
            delay -= Time.deltaTime;
        }
    }
    IEnumerator AttackScript()
    {
        yield return new WaitForSeconds(attackMotionTime);
        target.HitScript(Mathf.FloorToInt(AttackPower * Random.Range(0.9f, 1.1f)));
    }

    public override void HitScript(long dmg)
    {
        base.HitScript(dmg);
        if (HP_Bar != null)
        {
            HP_Bar.slider.value = (float)HP / MaxHP;
        }
    }
    public override void DieScript()
    {
        base.DieScript();
        state = States.Die;
        myAnim.SetTrigger("Die");
        Spawned.RemoveSpawnerList(this);
        action?.Invoke(monsterID);
        if (DropTable.Length > 0)
        {
            StartCoroutine(DropItems());
        }
        QuestSlot2.Instance.TextChange();
        if (ChaosDungeon.Instance != null)
        {
            ChaosDungeon.Instance.GiveExpGold(giveExp, giveGold);
        }
    }

    IEnumerator DropItems()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        HP_Bar.gameObject.SetActive(false);
        DropItem dr = Instantiate(Resources.Load<DropItem>("Item/DropItem"), transform.position + new Vector3(Random.Range(-1f, 1f),
            0, Random.Range(-1f, 1f)), Quaternion.identity);
        dr.itemData = DropTable[Random.Range(0, DropTable.Length)];
        
        if(dr.itemData.Type != ItemData.ItemType.Equip)
        {
            dr.itemCount = Random.Range(1, 3);
        }
        else
        {
            dr.itemCount = 1;
        }
        dr.Initialize();
        
    }

    private void OnDestroy()
    {
        if (HP_Bar != null)
        {
            Destroy(HP_Bar.gameObject);
        }
        if (icon != null)
        {
            Destroy(icon.gameObject);
        }
    }
}
