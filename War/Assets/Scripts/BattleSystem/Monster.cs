
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Monster : BattleData
{
    [Header("몬스터 id는 겹치지 않게 해라.")]
    public int monsterID;
    public enum States {
    Normal, Battle, Attack, Die
    }
    public States state;
    public int level;
    protected BattleData target;
    [SerializeField] protected float delay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected new void Start()
    {
        HP = MaxHP;
        
        gameObject.layer = 8;
        MoveCo = null;
        base.Start();
    }
    protected void Follow()
    {
        StopMove();
        myAnim.SetBool("IsRun", true);
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, path))
        {
            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                transform.LookAt(path.corners[1]);
                transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
                transform.Translate(transform.forward * Time.deltaTime * moveSpeed, Space.World);
            }
        }
    }

    protected IEnumerator Move()
    {
        myAnim.SetBool("IsRun", true);

        Vector3 pos = transform.position + new Vector3(UnityEngine.Random.Range(-4f, 4f), 0, UnityEngine.Random.Range(-4f, 4f));
        yield return MoveCo2 = StartCoroutine(PathRun(pos, moveSpeed));
        myAnim.SetBool("IsRun", false);

        yield return new WaitForSeconds(1.5f);
        MoveCo = null;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            BattleData bd = other.GetComponent<BattleData>();
            if (bd != null)
            {
                target = bd;
                state = States.Battle;
            }
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            BattleData bd = other.GetComponent<BattleData>();
            if (bd == target)
            {
                target = null;
                state = States.Normal;

            }
        }
    }
    public override void HitScript(long dmg)
    {
        if (HP > 0)
        {
            HP -= dmg;
            TextMeshProUGUI txt = Instantiate(Resources.Load<TextMeshProUGUI>("DmgText"), Camera.main.WorldToScreenPoint(DamagePos.position), Quaternion.identity);
            txt.text = dmg.ToString("N0");
            txt.color = Color.white;
            if (HP <= 0)
            {
                HP = 0;
                DieScript();
            }
           
        }
    }

    public override void DieScript()
    {
        StopAllCoroutines();
    }

}
