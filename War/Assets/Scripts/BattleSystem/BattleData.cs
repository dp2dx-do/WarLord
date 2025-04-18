using System.Collections;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class BattleData : MonoBehaviour
{
    public Animator myAnim;
    public Sprite MinimapIconSprite;
    protected MinimapIcon icon;
    public event UnityAction changeHP;
    [SerializeField] long _hp;
    public long HP { get => _hp;
        set
        {
            _hp = value;
            changeHP?.Invoke();
        } 
    }
    public long MaxHP { get; protected set; }
    public long AttackPower { get; protected set; }
    public float moveSpeed { get; protected set; }
    [SerializeField] protected float SetMoveSpeed;
    public Transform DamagePos;

    public Coroutine MoveCo, MoveCo2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        moveSpeed = SetMoveSpeed;
        HP = MaxHP;
        myAnim = GetComponentInChildren<Animator>();
        if (MinimapIconSprite!= null)
        {
            icon = Instantiate(Resources.Load<MinimapIcon>("UI/IconMinimap"), Minimap.Instance.transform);
            icon.Target = transform;
            icon.Set(transform, MinimapIconSprite);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopMove()
    {
        if (MoveCo != null)
        {
            StopCoroutine(MoveCo);
            MoveCo = null;
        }
        if (MoveCo2 != null)
        {
            StopCoroutine(MoveCo2);
            MoveCo2 = null;
        }
    }
    public virtual void HitScript(long dmg)
    {
        
    }
    public virtual void DieScript()
    {
        myAnim.SetTrigger("Die");
    }
    public IEnumerator PathRun(Vector3 pos, float speed)
    {
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(transform.position, pos, NavMesh.AllAreas, path))
        {
            if (path.status != NavMeshPathStatus.PathInvalid)
            {

                int ii = 0;
                while (ii < path.corners.Length - 1)
                {
                    Vector3 dir = path.corners[ii + 1] - transform.position;
                    dir.y = 0;
                    dir.Normalize();
                    float angle = Vector3.Angle(transform.forward, dir);
                    float rotDir = Vector3.Dot(transform.right, dir) < 0f ? -1f : 1f;
                    while (angle > 0f)
                    {
                        float delta = Mathf.Min(Time.deltaTime * 1080f, angle);
                        transform.Rotate(Vector3.up * delta *rotDir, Space.World);
                        angle -= delta;
                        //yield return null;
                    }
                    float dist = Vector3.Distance(transform.position, path.corners[ii + 1]);
                    while (dist > 0)
                    {
                        float delta = Time.deltaTime * speed;
                        if (delta > dist) delta = dist;
                        transform.Translate(dir * delta, Space.World);
                        dist -= delta;
                        yield return null;
                    }
                    ii++;
                }
            }
        }
        else {
        
        } 
        ;
        MoveCo2 = null;
    }

    private void OnDestroy()
    {
        if (icon != null)
        {
            Destroy(icon.gameObject);
        }
    }
}
