using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject enemy_Container;
    [SerializeField] int enemySpawnRate = 0;
    [SerializeField] GameObject[] powerups;
    private bool stopSpawning = false;
    void Start()
    {
        
    }
    IEnumerator SpawnEnemyRoutine()
    {
        while (stopSpawning == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 8f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            newEnemy.transform.parent = enemy_Container.transform;
            yield return new WaitForSeconds(enemySpawnRate);
        }
           
    }
    IEnumerator SpwanPowerUpRoutine()
    {
        while (stopSpawning == false)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 8f, 0);
            int randomPowerups = Random.Range(0, 3);
            GameObject newPowerUp = Instantiate(powerups[randomPowerups], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
    public void onPlayerDeath()
    {
        stopSpawning = true;
    }
    public void startSpawn()
    {       
            StartCoroutine(SpawnEnemyRoutine());
            StartCoroutine(SpwanPowerUpRoutine());                
    }
}
