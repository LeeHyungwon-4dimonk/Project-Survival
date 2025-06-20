using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _maxHydration = 100f;
    [SerializeField] private float _maxHunger = 100f;
    [SerializeField] private float _moveSpeed = 5f;

    private float _currentHealth;
    private float _currentHydration;
    private float _currentHunger;

    public float Health => _currentHealth;
    public float Hydration => _currentHydration;
    public float Hunger => _currentHunger;
    public float MoveSpeed => _moveSpeed;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _currentHydration = _maxHydration;
        _currentHunger = _maxHunger;
    }

    private void Update()
    {
        DecreaseSurvivalStats();
        CheckDeath();
    }


    //���а� ��� ���� ��ġ�� ���а� ��Ⱑ 0�϶� �����ϴ� ü���� ���� ���Ƿ� ���س���.
    //�������Ӹ��� ���а� ��� ����. ������ 0�̰ų� ��Ⱑ 0�϶� ü�� ����.
    private void DecreaseSurvivalStats()
    {
        float decreaseRate = Time.deltaTime;

        _currentHydration -= decreaseRate * 1f;
        _currentHunger -= decreaseRate * 0.8f;

        _currentHydration = Mathf.Clamp(_currentHydration, 0, _maxHydration);
        _currentHunger = Mathf.Clamp(_currentHunger, 0, _maxHunger);

        if (_currentHydration <= 0 || _currentHunger <= 0)
        {
            _currentHealth -= decreaseRate * 5f;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        }
    }

    private void CheckDeath()
    {
        if (_currentHealth <= 0f)
        {
            Debug.Log("�÷��̾� ���");
        }
    }

    public void SaveStatsToData(SaveData data)
    {
        data.currentHealth = _currentHealth;
        data.currentHydration = _currentHydration;
        data.currentHunger = _currentHunger;
    }

    //�ҷ������
    public void ApplySavedStats(SaveData data)
    {
        _currentHealth = Mathf.Clamp(data.currentHealth, 0, _maxHealth);
        _currentHydration = Mathf.Clamp(data.currentHydration, 0, _maxHydration);
        _currentHunger = Mathf.Clamp(data.currentHunger, 0, _maxHunger);
    }
}
