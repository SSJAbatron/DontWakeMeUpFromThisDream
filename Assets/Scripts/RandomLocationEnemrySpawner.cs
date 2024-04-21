using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLocationEnemrySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemy;
    public int spawn_interval;
    public int spawn_count_limit;
    public bool canSpawn;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy(spawn_interval));
    }

    // Update is called once per frame
    void Update()
    {
        // test function to destroy enemies
        
        if(FindObjectsOfType <Enemy>().Length < spawn_count_limit)
        {
            canSpawn = true;
        }
        
        if(FindObjectsOfType<Enemy>().Length > spawn_count_limit)
        {
            canSpawn = false;
        }

        Debug.Log(canSpawn.ToString());
    }

    private IEnumerator SpawnEnemy(int interval)
    {
        while(canSpawn)
        {
            yield return new WaitForSeconds(interval);
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            GameObject enemySpawned = Instantiate(enemy, spawnPoints[randomSpawnPoint].position, transform.rotation);
        }
    }
}
