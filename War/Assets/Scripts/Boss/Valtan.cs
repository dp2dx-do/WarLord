using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.AI;



public class Valtan : BossMonster
{
    
    [SerializeField] GameObject Effect;
    [SerializeField] float AttackRange;
    bool isAttack = false;
    bool isPush = false;

    new void Start()
    {
        base.Start();
        HP = MaxHP;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (HP > 0)
        {
            if (state == States.Battle)
            {
                if (!isAttack)
                {
                    if (Vector3.Distance(transform.position, target.transform.position) > AttackRange)
                    {
                        Follow();
                    }
                }
                if (delay < 0 && Vector3.Distance(target.transform.position, transform.position) < AttackRange)
                {
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            StartCoroutine(FireAttack());
                            delay = 6f;
                            break;
                        case 1:
                            StartCoroutine(Push());
                            delay = 5f;
                            break;
                    }
                }
            }
            else if (state == States.Normal && MoveCo == null)
            {
                MoveCo = StartCoroutine(Move());
            }
            delay -= Time.deltaTime;
        }
    }
    


    private void OnCollisionEnter(Collision collision)
    {
        if (isPush)
        {
            if(collision.gameObject.layer == 7)
            {
                BattleData bd = collision.gameObject.GetComponent<BattleData>();
                if (bd != null)
                {
                    bd.HitScript(Mathf.FloorToInt(AttackPower * Random.Range(0.9f, 1.1f)));
                }
            }
        }
    }

    IEnumerator FireAttack()
    {
        
        myAnim.SetBool("IsRun", false);
        isAttack = true;
        myAnim.SetTrigger("Taunt");
        Vector3 look = target.transform.position;
        look.y = 0;
        transform.LookAt(look);
        
        
        yield return new WaitForSeconds(1f);
        myAnim.SetTrigger("OnAttack1");
        yield return new WaitForSeconds(0.25f);
        GameObject eff = Instantiate(Effect, transform.position, transform.rotation);
        Destroy(eff, 1f);
        yield return new WaitForSeconds(0.1f);
        Collider[] colls = Physics.OverlapCapsule(transform.position+ transform.forward * 2f, transform.position + transform.forward * 5f, 1.25f, LayerMask.GetMask("Player"));
        for (int i = 0; i < colls.Length; i++)
        {
            BattleData bd = colls[i].gameObject.GetComponent<BattleData>();
            if (bd != null)
            {
                bd.HitScript(Mathf.FloorToInt(AttackPower * Random.Range(0.9f, 1.1f)));
                
            }
        }
        isAttack = false;
        myAnim.SetBool("IsRun", true);
    }

    IEnumerator Push()
    {
        myAnim.SetBool("IsRun", false);
        isAttack = true;
        isPush = true;
        myAnim.SetTrigger("Taunt");

        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, path))
        {
            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                transform.LookAt(path.corners[1]);
                transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
                yield return new WaitForSeconds(1f);
                float t = 0;
                while (t < 0.75f)
                {
                    if (NavMesh.CalculatePath(transform.position, transform.position + transform.forward * 2.5f, NavMesh.AllAreas, path))
                    {
                        if (path.status == NavMeshPathStatus.PathInvalid)
                        {
                            break;
                        }
                    }
                    else break;
                    transform.Translate(transform.forward * Time.deltaTime * 12f, Space.World);
                    yield return null;
                    t += Time.deltaTime;
                }
            }
        }
        myAnim.SetBool("IsRun", true);
        isAttack = false;
        isPush = false;
    }
}
