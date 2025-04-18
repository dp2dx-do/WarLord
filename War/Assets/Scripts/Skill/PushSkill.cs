using System.Collections;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.AI;

public class PushSkill : Skills
{
    public float AttackTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Attack());
        StartCoroutine(Push());
        Destroy(gameObject, DestroyTime);
        SoundManager.instance.Effect.PlayOneShot(soundFx);
        transform.parent = usedPlayer.transform;
    }
    IEnumerator Push()
    { 
        float t = 0;
        while (t < AttackTime)
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Ground"));
           
            if(Vector3.Dot(hit.point- usedPlayer.transform.position, usedPlayer.transform.right) > 0.2f)
            {
                usedPlayer.transform.Rotate(Vector3.up * Time.deltaTime * 120f);
            }
            if (Vector3.Dot(hit.point - usedPlayer.transform.position, usedPlayer.transform.right) < 0.2f)
            {
                usedPlayer.transform.Rotate(Vector3.up * -Time.deltaTime * 120f);
            }
            if (battleDatas.Count <= 0)
            {
                NavMeshPath path = new NavMeshPath();
                if (NavMesh.CalculatePath(transform.position, transform.position + transform.forward, NavMesh.AllAreas, path))
                    
                    usedPlayer.transform.Translate(usedPlayer.transform.forward * Time.deltaTime * (4f + t * 2.5f), Space.World);
            }
            yield return null;
            t += Time.deltaTime;
        }
    }
    IEnumerator Attack()
    {
        usedPlayer.myAnim.SetTrigger("Slice");
        yield return new WaitForSeconds(0.05f);
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

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
