using System.Collections;
using UnityEngine;

public class WhirlWind : Skills
{
    public float AttackTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Attack());
        Destroy(gameObject, DestroyTime);

    }
    IEnumerator Attack()
    {
        usedPlayer.myAnim.SetBool("IsSpinning", true);
        yield return new WaitForSeconds(0.05f);
        SoundManager.instance.Effect.PlayOneShot(soundFx);

        float t = 0;
        while (t < AttackTime)
        {
            foreach (BattleData battleData in battleDatas)
            {
                if (battleData != null)
                {
                    battleData.HitScript(RandomDMG());
                }
            }
            t += 0.25f;
            yield return new WaitForSeconds(0.25f);
        }
        Effect.gameObject.SetActive(false);
        usedPlayer.myAnim.SetBool("IsSpinning", false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
