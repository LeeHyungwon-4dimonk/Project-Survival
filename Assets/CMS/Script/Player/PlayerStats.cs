using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _maxSaturation = 100f;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _runSpeed = 7f;
    [SerializeField] private float _maxStamina = 100f;
    [SerializeField] private float _staminaDecreaseRate = 10f;
    [SerializeField] private float _staminaRecoveryRate = 10f;
    [SerializeField] private float _maxInventoryWeight = 50f;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _dieDirectionParam = "DeathDirection";
    [SerializeField] private string _isDeadParam = "IsDead";
    [SerializeField] private int _dieDirection = 0;

    private PlayerMovement _playerMovement;
    private float _currentHealth;
    private float _currentSaturation;
    private float _currentStamina;
    private float _currentInventoryWeight = 0f;
    private bool _isDead = false;

    public bool IsStaminaEmpty => _currentStamina <= 0f;
    public float MaxHealth => _maxHealth;
    public float Health => _currentHealth;
    public float Saturation => _currentSaturation;
    public float MaxSaturation => _maxSaturation;
    public float MoveSpeed => _moveSpeed;
    public float RunSpeed => _runSpeed;
    public float MaxStamina => _maxStamina;
    public float CurrentStamina => _currentStamina;
    public float StaminaDecreaseRate => _staminaDecreaseRate;
    public float StaminaRecoveryRate => _staminaRecoveryRate;
    public float MaxInventoryWeight => _maxInventoryWeight;
    public float CurrentInventoryWeight => _currentInventoryWeight;


    private void Awake()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
        _playerMovement = GetComponent<PlayerMovement>();

        _currentHealth = _maxHealth;
        _currentSaturation = _maxSaturation;
        _currentStamina = _maxStamina;
    }

    private void Start()
    {
        StartCoroutine(SurvivalStatRoutine());
    }

    private IEnumerator SurvivalStatRoutine()
    {
        while (!_isDead)
        {
            DecreaseSurvivalStats();
            CheckDeath();

            Debug.Log($"Health: {_currentHealth}, Saturation: {_currentSaturation}, Stamina: {_currentStamina}");

            yield return new WaitForSeconds(1f);
        }
    }

    private void DecreaseSurvivalStats()
    {
        float saturationDecreaseRate = 0.3f;
        float decreaseRate = 1f;

        _currentSaturation -= saturationDecreaseRate;
        _currentSaturation = Mathf.Clamp(_currentSaturation, 0, _maxSaturation);

        if (_currentSaturation <= 0)
        {
            _currentHealth -= decreaseRate * 5f;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        }
    }

    private void CheckDeath()
    {
        if (_currentHealth <= 0f && !_isDead)
        {
            Debug.Log("플레이어 사망 감지됨");
            StartCoroutine(DieRoutine());
        }
    }

    private IEnumerator DieRoutine()
    {
        if (_isDead) yield break;
        _isDead = true;

        int direction = GetDieDirection();

        Debug.Log($"[DieRoutine] 방향 설정: {direction}");

        _animator.SetInteger(_dieDirectionParam, direction);
        _animator.SetBool(_isDeadParam, true);

        Debug.Log($"Animator 파라미터 설정 완료 - DeathDirection: {direction}, IsDead: true");


        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }

    private int GetDieDirection()
    {
        if (_playerMovement == null)
            return 0;

        Vector2 dir = _playerMovement.LastMoveDirection;

        if (dir.sqrMagnitude < 0.01f)
        {
            dir = Vector2.down;
        }

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            return dir.x > 0 ? 3 : 2;
        }
        else
        {
            return dir.y > 0 ? 0 : 1;
        }
    }

    public void SetDieDirection(int direction)
    {
        _dieDirection = direction;
        Debug.Log($"SetDieDirection 호출됨: {_dieDirection}");
    }

    public void TakeDamage(float damage)
    {
        if (_isDead) return;

        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        Debug.Log($"플레이어가 {damage} 피해를 받았습니다. 현재 체력: {_currentHealth}");

        CheckDeath();
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

    public void RecoverHealth(float amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
    }

    public void RecoverSaturation(float amount)
    {
        _currentSaturation += amount;
        _currentSaturation = Mathf.Clamp(_currentSaturation, 0, _maxSaturation);
    }

    public bool AddInventoryWeight(float amount)
    {
        if (_currentInventoryWeight + amount > _maxInventoryWeight)
        {
            Debug.LogWarning("인벤토리 무게 초과! 아이템을 더 들 수 없습니다.");
            return false;
        }

        _currentInventoryWeight += amount;
        return true;
    }

    public void RemoveInventoryWeight(float amount)
    {
        _currentInventoryWeight -= amount;
        _currentInventoryWeight = Mathf.Clamp(_currentInventoryWeight, 0, _maxInventoryWeight);
    }
    public void IncreaseMaxInventoryWeight(float amount)
    {
        _maxInventoryWeight += amount;
        _maxInventoryWeight = Mathf.Max(0f, _maxInventoryWeight);
    }
}
