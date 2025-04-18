using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.LowLevel;

public class Skills : MonoBehaviour
{
    protected System.Random random = new System.Random();

    public PlayerScript usedPlayer;
    [HideInInspector] public long Damage;
    public int UseMP;
    public double Multipler;
    public float DestroyTime;
    public float CoolTime, Delay, EffectDelay;
    protected List<BattleData> battleDatas = new List<BattleData>();
    public Collider skillRange;
    public GameObject Effect;
    public AudioClip soundFx;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Attack());
        Destroy(gameObject, DestroyTime);
    }


    IEnumerator Attack()
    {
        skillRange.enabled = false;
        usedPlayer.myAnim.SetTrigger("Chop");
        yield return new WaitForSeconds(EffectDelay);
        skillRange.enabled = true;
        Effect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        foreach (BattleData battleData in battleDatas)
        {
            if (battleData != null)
            {
                battleData.HitScript(Damage);
            }
        }
        SoundManager.instance.Effect.PlayOneShot(soundFx);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            BattleData bd = other.GetComponent<BattleData>();
            if (bd != null && !battleDatas.Contains(bd))
            {
                battleDatas.Add(bd);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            BattleData bd = other.GetComponent<BattleData>();
            if (bd != null && battleDatas.Contains(bd))
            {
                battleDatas.Remove(bd);
            }
        }
    }
    
    protected long RandomDMG()
    {

        return (long)(Damage *(random.NextDouble() * 0.1 + 0.9));
    }
    
}
