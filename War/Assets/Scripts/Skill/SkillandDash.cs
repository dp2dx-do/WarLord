using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class SkillandDash : Skills
{
    public float DashDist, DashTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        usedPlayer.myAnim.SetTrigger("JumpChop");

        StartCoroutine(Dash());
        Destroy(gameObject, DestroyTime);
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (BattleData battleData in battleDatas)
        {
            if (battleData != null)
            {
                battleData.HitScript(RandomDMG());
            }
        }
        SoundManager.instance.Effect.PlayOneShot(soundFx);

    }
    IEnumerator Dash()
    {
        StartCoroutine(Attack());
        float t = 0;
        NavMeshPath path = new NavMeshPath();
        while (NavMesh.CalculatePath(usedPlayer.transform.position, usedPlayer.transform.position + usedPlayer.transform.forward * (DashDist > 0? 1 : -1), NavMesh.AllAreas, path) 
            && t<DashTime)
        {
            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
                usedPlayer.transform.Translate(usedPlayer.transform.forward * Time.deltaTime * (DashDist / DashTime), Space.World);
                t += Time.deltaTime;
                yield return null;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
