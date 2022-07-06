using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPointLeft;
    [SerializeField] List<Transform> spawnPointRight;
    [SerializeField] GameObject EnemyPrefabLeft;
    [SerializeField] GameObject EnemyPrefabRight;
    public List<EnemyAttackManager> listEnemyAlive = new List<EnemyAttackManager>();
    [SerializeField] GameObject chest;

    public int totalSpawn = 0;
    public int totalKill = 0;
    [SerializeField] UICountDown ui;

    private void Start()
    {
        chest.SetActive(false); 
        StartCoroutine(StartCountDown());
    }

    private void Update()
    {
        foreach(EnemyAttackManager m in listEnemyAlive)
        {
            if(m == null)
            {
                continue;
            }
            if(m.isDead)
            {
                totalKill += 1;
                listEnemyAlive.Remove(m);
                Debug.Log("Enemy dead");
                if (totalKill == 2)
                {
                    Debug.Log("Spawn 3 more enemy");
                    Spawn();
                    Spawn();
                    Spawn();
                }
                if(totalKill == 4)
                {
                    Debug.Log("Spawn 5 more enemy");
                    Spawn();
                    Spawn();
                    Spawn();
                    Spawn();
                    Spawn();
                }
                if(totalKill == 10)
                {
                    Finish();
                }
                return;
                
            }
        }
    }

    public void Finish()
    {
        chest.SetActive(true);
    }

    IEnumerator StartCountDown()
    {
        for(int i = 15;i >= 0;i--)
        {
            ui.SetCountTo(i);
            yield return new WaitForSeconds(1);
        }
        for(int i = 0; i < 10; i++)
        {
            StartCoroutine(SpawnInSeconds(Mathf.Clamp(i*i-2*i,0,30)));
            Debug.Log("Spawning in : " + (i * i - 2 * i));
            
        }
        yield return new WaitForSeconds(35);
        if(IsEverythingSpawn() == false)
        {
            while(totalSpawn < 10)
            {
                Spawn();
            }
        }
       
        
    }
    bool IsEverythingSpawn()
    {
        return totalSpawn == 10;
        
    }

    IEnumerator SpawnInSeconds(int sec)
    {
        for (int i = 0; i < sec; i++)
        {
            yield return new WaitForSeconds(1);
        }
        Spawn();
    }

    void Spawn()
    {
        Debug.Log("Spawning");
        int rng = Random.Range(0, 100);
        if(rng < 50)
        {
            int index = Random.Range(0, spawnPointRight.Count);

            Vector3 spawnPoint = spawnPointRight[index].position;
            spawnPointRight.RemoveAt(index);
            
            GameObject enemy = Instantiate(EnemyPrefabLeft, spawnPoint, Quaternion.identity);
            listEnemyAlive.Add(enemy.GetComponentInChildren<EnemyAttackManager>());
        }
        else
        {
            int index = Random.Range(0, spawnPointLeft.Count);

            Vector3 spawnPoint = spawnPointLeft[index].position;
            
            spawnPointLeft.RemoveAt(index);


            GameObject enemy = Instantiate(EnemyPrefabRight, spawnPoint, Quaternion.identity);
            listEnemyAlive.Add(enemy.GetComponentInChildren<EnemyAttackManager>());
        }
        totalSpawn += 1;
    }


}
