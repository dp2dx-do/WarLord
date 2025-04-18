using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class KouKuSaton : BossMonster
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] GameObject FireEffect, PushEffect;
    [SerializeField] float AttackRange;
    bool isAttack = false;
    bool isPush = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
                            delay = 6f;
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
    IEnumerator FireAttack()
    {
        myAnim.SetBool("IsRun", false);
        isAttack = true;
        myAnim.SetTrigger("Taunt");
        Vector3 look = target.transform.position;
        look.y = 0;
        transform.LookAt(look);
        yield return new WaitForSeconds(1.75f);
        myAnim.SetTrigger("OnAttack1");
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < 8; i++)
        {
            GameObject eff = Instantiate(FireEffect, transform.position, transform.rotation * Quaternion.Euler(0, 45f * i, 0));
            Destroy(eff, 1f);
        }
        yield return new WaitForSeconds(0.1f);
        float radius = 5f;
        Collider[][] colls = {Physics.OverlapCapsule(transform.position + transform.forward * -radius,
            transform.position + transform.forward * radius,1f, LayerMask.GetMask("Player")),
            Physics.OverlapCapsule(transform.position + transform.right * -radius,
            transform.position + transform.right * radius,1f, LayerMask.GetMask("Player")),
            Physics.OverlapCapsule(transform.position + (transform.forward+transform.right)* -radius*0.71f ,
            transform.position + (transform.forward+transform.right)* radius*0.71f ,1f, LayerMask.GetMask("Player")),
            Physics.OverlapCapsule(transform.position + (transform.forward-transform.right) * -radius*0.71f,
            transform.position + (transform.forward-transform.right) * radius*0.71f,1f, LayerMask.GetMask("Player")) };

        for (int i = 0; i < colls.Length; i++)
        {
            for (int j = 0; j < colls[i].Length; j++)
            {
                BattleData bd = colls[i][j].gameObject.GetComponent<BattleData>();
                if (bd != null)
                {
                    bd.HitScript(Mathf.FloorToInt(AttackPower * Random.Range(0.9f, 1.1f)));
                }
            }

        }
        isAttack = false;
        myAnim.SetBool("IsRun", true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isPush)
        {
            if (collision.gameObject.layer == 7)
            {
                BattleData bd = collision.gameObject.GetComponent<BattleData>();
                if (bd != null)
                {
                    bd.HitScript(Mathf.FloorToInt(AttackPower * Random.Range(0.9f, 1.1f)));
                }
            }
        }
    }
    IEnumerator Push()
    {
        myAnim.SetBool("IsRun", false);
        isAttack = true;
        isPush = true;
        myAnim.SetTrigger("Taunt");
        //GameObject eff = Instantiate(PushEffect, transform.position, transform.rotation, transform);
        
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
