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


    //수분과 허기 감소 수치와 수분과 허기가 0일때 감소하는 체력의 양은 임의로 정해놓음.
    //매프레임마다 수분과 허기 감소. 수분이 0이거나 허기가 0일때 체력 감소.
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
            Debug.Log("플레이어 사망");
        }
    }

    public void SaveStatsToData(SaveData data)
    {
        data.currentHealth = _currentHealth;
        data.currentHydration = _currentHydration;
        data.currentHunger = _currentHunger;
    }

    //불러오기용
    public void ApplySavedStats(SaveData data)
    {
        _currentHealth = Mathf.Clamp(data.currentHealth, 0, _maxHealth);
        _currentHydration = Mathf.Clamp(data.currentHydration, 0, _maxHydration);
        _currentHunger = Mathf.Clamp(data.currentHunger, 0, _maxHunger);
    }
}
