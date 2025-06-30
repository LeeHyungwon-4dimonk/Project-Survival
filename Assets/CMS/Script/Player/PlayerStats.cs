using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _maxHydration = 100f;
    [SerializeField] private float _maxHunger = 100f;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _runSpeed = 7f;
    [SerializeField] private float _maxStamina = 100f;
    [SerializeField] private float _staminaDecreaseRate = 10f;   
    [SerializeField] private float _staminaRecoveryRate = 10f;

    private float _currentHealth;
    private float _currentHydration;
    private float _currentHunger;
    private float _currentStamina;
    public bool IsStaminaEmpty => _currentStamina <= 0f;
    public float Health => _currentHealth;
    public float Hydration => _currentHydration;
    public float Hunger => _currentHunger;
    public float MoveSpeed => _moveSpeed;
    public float RunSpeed => _runSpeed;
    public float MaxStamina => _maxStamina;
    public float CurrentStamina => _currentStamina;
    public float StaminaDecreaseRate => _staminaDecreaseRate;
    public float StaminaRecoveryRate => _staminaRecoveryRate;


    private void Awake()
    {
        _currentHealth = _maxHealth;
        _currentHydration = _maxHydration;
        _currentHunger = _maxHunger;
        _currentStamina = _maxStamina;
    }

    private void Start()
    {
        StartCoroutine(SurvivalStatRoutine());
    }

    private IEnumerator SurvivalStatRoutine()
    {
        while (_currentHealth > 0)
        {
            DecreaseSurvivalStats();
            CheckDeath();

            Debug.Log($"Health: {_currentHealth}, Hydration: {_currentHydration}, Hunger: {_currentHunger}, Stamina: {_currentStamina}");

            yield return new WaitForSeconds(1f); 
        }
    }

    private void DecreaseSurvivalStats()
    {
        float hydrationDecreaseRate = 0.5f; 
        float hungerDecreaseRate = 0.3f;  
        float decreaseRate = 1f; // Health Decrease Rate

        _currentHydration -= hydrationDecreaseRate;
        _currentHunger -= hungerDecreaseRate;

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
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        Debug.Log($"플레이어가 {damage}의 피해를 받았습니다. 현재 체력: {_currentHealth}");

        if(_currentHealth <= 0)
        {
            CheckDeath();
        }
    }

    public void DecreaseStamina(float amount)
    {
        _currentStamina -= amount;
        _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);
    }

    public void RecoverStamina(float amount)
    {
        _currentStamina += amount;
        _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);
    }
}
