using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{

    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth;

    public float Health => _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        
        Debug.Log($"���� �ǰ�: {damage}, ���� ü��: {_currentHealth}");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("���� ���");
        Destroy(gameObject);
    }
}
