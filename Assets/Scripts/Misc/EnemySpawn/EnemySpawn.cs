using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{
    public int MaxEnemies;
    public float SpawnDelay;
    public List<Enemy> Enemies;

    private int _enemyCount;
    private bool _spawning;
    private Vector3 _spawnArea;
     
    private void Start()
    {
        _spawning = false;
        _spawnArea = GetComponent<BoxCollider>().size / 2;
        RestartSpawningLoop();
    }

    private void RestartSpawningLoop()
    {
        if (_enemyCount < MaxEnemies && !_spawning)
        {
            StartCoroutine("SpawnEnemy");
        }
    }

    private IEnumerator SpawnEnemy()
    {
        _spawning = true;
        yield return new WaitForSeconds(SpawnDelay);
        CreateEnemy();
    }

    private void CreateEnemy()
    {
        var enemyFactory = new EnemyFactory(Enemies);
        var enemy = enemyFactory.CreateEnemy(0);
        var navMeshHelper = new NavMeshHelper();
        var randomPosition = navMeshHelper.GetRandomPositionWithArea(transform.position, new Vector3(_spawnArea.x, _spawnArea.z));

        if(randomPosition != Vector3.zero)
        {
            var spawnedEnemy = Instantiate(enemy, randomPosition, Quaternion.identity, transform);
            _enemyCount++;
            _spawning = false;
        }
        RestartSpawningLoop();
    }
   
    public void EnemyDied()
    {
        _enemyCount--;
        RestartSpawningLoop();
    }
}
