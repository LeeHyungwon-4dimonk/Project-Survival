using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
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

    [Header("Sounds")]
    [Tooltip("피격 시 재생할 AudioClip")]
    [SerializeField] private AudioClip damageClip;

    private AudioSource _audioSource;
    private PlayerMovement _playerMovement;

    private float _currentHealth;
    private float _currentSaturation;
    private float _currentStamina;
    private float _currentInventoryWeight = 0f;
    private bool _isDead = false;
    private bool _isFlashing = false;
    private Coroutine _flashCoroutine;

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
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;

        if (_animator == null)
            _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();

        _currentHealth = _maxHealth;
        _currentSaturation = _maxSaturation;
        _currentStamina = _maxStamina;
    }

    private void Start()
    {
        GameManager.Instance.PlayerStats = this;
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
        if (GameManager.Instance.DayNightManager.IsInBase)
        {
            return;
        }

        const float saturationDecreaseRate = 0.3f;
        const float healthDamageRate = 5f;

        _currentSaturation -= saturationDecreaseRate;
        _currentSaturation = Mathf.Clamp(_currentSaturation, 0f, _maxSaturation);

        if (_currentSaturation <= 0f)
        {
            _currentHealth -= healthDamageRate;
            _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);
        }
    }

    private void CheckDeath()
    {
        if (_currentHealth <= 0f && !_isDead)
        {
            StartCoroutine(DieRoutine());
        }
    }

    private IEnumerator DieRoutine()
    {
        if (_isDead) yield break;
        _isDead = true;

        int direction = GetDieDirection();
        _animator.SetInteger(_dieDirectionParam, direction);
        _animator.SetBool(_isDeadParam, true);

        DeathPanelController.Instance.ShowDeathPanel();

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private int GetDieDirection()
    {
        if (_playerMovement == null)
            return 0;

        Vector2 dir = _playerMovement.LastMoveDirection;
        if (dir.sqrMagnitude < 0.01f) dir = Vector2.down;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            return dir.x > 0 ? 3 : 2;
        else
            return dir.y > 0 ? 0 : 1;
    }

    public void SetDieDirection(int direction)
    {
        _animator.SetInteger(_dieDirectionParam, direction);
	// _dieDirection = direction;
    }

    public void TakeDamage(float damage)
    {
        if (_isDead) return;

        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);

        if (damageClip != null)
            _audioSource.PlayOneShot(damageClip);

        if (_flashCoroutine == null)
            _flashCoroutine = StartCoroutine(FlashSprite());

        CheckDeath();
    }

    public void DecreaseStamina(float amount)
    {
        _currentStamina -= amount;
        _currentStamina = Mathf.Clamp(_currentStamina, 0f, _maxStamina);
    }

    public void RecoverStamina(float amount)
    {
        _currentStamina += amount;
        _currentStamina = Mathf.Clamp(_currentStamina, 0f, _maxStamina);
    }

    public void RecoverHealth(float amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);
    }

    public void RecoverSaturation(float amount)
    {
        _currentSaturation += amount;
        _currentSaturation = Mathf.Clamp(_currentSaturation, 0f, _maxSaturation);
    }

    public bool AddInventoryWeight(float amount)
    {
        if (_currentInventoryWeight + amount > _maxInventoryWeight)
        {
            return false;
        }
        _currentInventoryWeight += amount;
        _currentInventoryWeight = Mathf.Round(_currentInventoryWeight * 10) * 0.1f;
        return true;
    }

    public void RemoveInventoryWeight(float amount)
    {
        _currentInventoryWeight -= amount;
        _currentInventoryWeight = Mathf.Round(_currentInventoryWeight * 10) * 0.1f;
        _currentInventoryWeight = Mathf.Clamp(_currentInventoryWeight, 0f, _maxInventoryWeight);
    }

    public void IncreaseMaxInventoryWeight(float amount)
    {
        _maxInventoryWeight += amount;
        _maxInventoryWeight = Mathf.Max(0f, _maxInventoryWeight);
    }

    private IEnumerator FlashSprite()
    {
        _isFlashing = true;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer == null)
        {
            _isFlashing = false;
            yield break;
        }

        Color originalColor = renderer.color;
        Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.2f);

        for (int i = 0; i < 2; i++)
        {
            renderer.color = transparentColor;
            yield return new WaitForSeconds(0.05f);
            renderer.color = originalColor;
            yield return new WaitForSeconds(0.05f);
        }

        renderer.color = originalColor;
        _isFlashing = false;
        _flashCoroutine = null;
    }


}
