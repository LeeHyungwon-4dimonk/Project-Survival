using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _monsterPrefab;
    [SerializeField] private float _respawnDelay = 5f;
    [SerializeField] private Transform[] _patrolPoints;

    private Vector2 _spawnPoint;

    private void Start()
    {
        _spawnPoint = transform.position;
        SpawnMonster();
    }

    private void SpawnMonster()
    {
        GameObject monster = Instantiate(_monsterPrefab, _spawnPoint, Quaternion.identity);

        MonsterStats stats = monster.GetComponent<MonsterStats>();
        if (stats != null)
        {
            stats.Spawner = this;
        }

        Monster monsterScript = monster.GetComponent<Monster>();
        if (monsterScript != null)
        {
            monsterScript.SetPatrolPoints(_patrolPoints);
        }
    }

    public void OnMonsterDied()
    {
        Debug.Log("몬스터가 죽었음 - 리스폰 대기 시작");
        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(_respawnDelay);
        SpawnMonster();
    }
}
