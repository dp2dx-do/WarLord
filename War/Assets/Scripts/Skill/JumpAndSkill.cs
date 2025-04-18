using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.AI;

public class JumpAndSkill : Skills
{
    public float JumpTime;
    public float JumpHeight;
    public float moveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        usedPlayer.myAnim.SetTrigger("Jump");
        StartCoroutine(Jump());
        Destroy(gameObject, DestroyTime);

    }

    IEnumerator Jump()
    {
        NavMeshPath path = new NavMeshPath();
        float t = 0;
        Vector3 forw = usedPlayer.transform.forward;
        Vector3 pos = usedPlayer.transform.position;
        forw.y = 0;
        while (t < JumpTime)
        {
            Vector3 pos2 = usedPlayer.transform.position; 
            usedPlayer.transform.forward = forw;
            if (NavMesh.CalculatePath(pos2, pos2 + usedPlayer.transform.forward, NavMesh.AllAreas, path))
            {
                if (path.status != NavMeshPathStatus.PathInvalid)
                {
                    if(!Physics.Raycast(usedPlayer.transform.position, Vector3.down, 3f, 1 << 8)&&
                        !Physics.Raycast(usedPlayer.transform.position+transform.forward*0.5f, Vector3.down, 3f, 1 << 8))
                    {
                        usedPlayer.transform.Translate(usedPlayer.transform.forward * Time.deltaTime * moveSpeed, Space.World);
                        usedPlayer.transform.position = new Vector3(usedPlayer.transform.position.x, pos.y + (JumpHeight * 4f / (JumpTime * JumpTime)) * t * (JumpTime - t), usedPlayer.transform.position.z);
                        t += Time.deltaTime;
                        yield return null;

                    }
                    else
                    {
                        usedPlayer.transform.position = new Vector3(usedPlayer.transform.position.x, pos.y, usedPlayer.transform.position.z);
                        break;
                    }
                }
                else
                {
                    usedPlayer.transform.position = new Vector3(usedPlayer.transform.position.x, pos.y, usedPlayer.transform.position.z);
                    break;
                }
            }
            else
            {
                usedPlayer.transform.position = new Vector3(usedPlayer.transform.position.x, pos.y, usedPlayer.transform.position.z);
                break;
            }
        }
        usedPlayer.myAnim.SetTrigger("EndJump");
        skillRange.enabled = true;
        StartCoroutine(Attack());
        transform.position = usedPlayer.transform.position;
        Effect.SetActive(true);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
