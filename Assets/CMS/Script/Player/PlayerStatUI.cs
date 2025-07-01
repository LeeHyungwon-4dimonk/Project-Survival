using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _hydrationBar;
    [SerializeField] private Slider _hungerBar;
    [SerializeField] private PlayerStats _playerStats;

    private void Start()
    {
        // MaxValue ¼³Á¤
        _healthBar.maxValue = _playerStats.Health;
        _hydrationBar.maxValue = _playerStats.Hydration;
        _hungerBar.maxValue = _playerStats.Hunger;
    }

    private void Update()
    {
        _healthBar.value = _playerStats.Health;
        _hydrationBar.value = _playerStats.Hydration;
        _hungerBar.value = _playerStats.Hunger;
    }
}
