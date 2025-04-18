using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    public NormalMonster[] spawnMonsters;
    public event UnityAction<int> OnSpawn;
    [SerializeField] int NumberOfMonster;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i=0; i<NumberOfMonster; i++)
        {
            StartCoroutine(Respawn(3f));
        }
        
        
    }
    public void RemoveSpawnerList(NormalMonster nm)
    {
        StartCoroutine(Respawn(7.5f, nm));
    }
    IEnumerator Respawn(float time, NormalMonster nm = null)
    {
        yield return new WaitForSeconds(time);
        if (nm != null) 
        {
            nm.gameObject.SetActive(true);
            nm.transform.position = transform.position + Vector3.forward * Random.Range(-1f, 1f) +
                Vector3.right * Random.Range(-1f, 1f);
        }
        else
        {
            nm = Instantiate(spawnMonsters[Random.Range(0, spawnMonsters.Length)], transform.position + Vector3.forward * Random.Range(-1f, 1f) +
                Vector3.right * Random.Range(-1f, 1f), Quaternion.identity);
            nm.Spawned = this;
        }
        if(nm.action == null)
        {
            nm.action += OnSpawn;
        }
        if (GameData.Instance.playerData.isTutorialOpen)
        {
            nm.action += i => { FindFirstObjectByType<TutorialPage6>().action.Invoke(); };
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
