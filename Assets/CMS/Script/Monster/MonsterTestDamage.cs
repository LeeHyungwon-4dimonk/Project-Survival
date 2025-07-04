using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTestDamage : MonoBehaviour
{
    [SerializeField] private float _testDamage = 10f;
    [SerializeField] private KeyCode _damageKey = KeyCode.Space;

    private MonsterStats _monsterStats;

    private void Awake()
    {
        _monsterStats = GetComponent<MonsterStats>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_damageKey))
        {
            if (_monsterStats != null)
            {
                _monsterStats.TakeDamage(_testDamage);
                Debug.Log($"[�׽�Ʈ] {_damageKey} ������ {_testDamage} ���ظ� ����!");
            }
        }
    }
}
