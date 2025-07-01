using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _staminaBar;
    [SerializeField] private Slider _saturationBar;
    [SerializeField] private PlayerStats _playerStats;

    private void Start()
    {
        _healthBar.maxValue = _playerStats.MaxHealth;
        _staminaBar.maxValue = _playerStats.MaxStamina;
        _saturationBar.maxValue = _playerStats.MaxSaturation;
    }

    private void Update()
    {
        _healthBar.value = _playerStats.Health;
        _staminaBar.value = _playerStats.CurrentStamina;
        _saturationBar.value = _playerStats.Saturation;
    }
}
