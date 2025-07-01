using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DayNightCycleManager : MonoBehaviour
{
    #region Configurable Time Settings

    private const float TotalDayDuration = 600f;
    private const float DayDuration = 420f;
    private const float NightDuration = 180f;

    #endregion





    #region State

    private float _timePassed = 0f;
    private int _currentDay = 1;
    private bool _isDay = true;

    #endregion





    #region Properties

    public int CurrentDay => _currentDay;
    public bool IsDayTime => _isDay;

    #endregion





    #region Serialized Fields

    [SerializeField] private Image _darkOverlay;
    [SerializeField] private float _fadeDuration = 2f;

    #endregion // Serialized Fields





    #region mono funcs

    private void Start()
    {
        StartCoroutine(DayNightCycleRoutine());
    }

    #endregion // mono funcs





    #region coroutines

    private IEnumerator DayNightCycleRoutine()
    {
        while (true) {
            yield return new WaitForSeconds(1f);
            _timePassed += 1f;

            if (_timePassed >= TotalDayDuration) {
                _timePassed -= TotalDayDuration;
                _currentDay++;
            }

            bool isNowDay = _timePassed < DayDuration;
            if (_isDay != isNowDay) {
                _isDay = isNowDay;
                StartCoroutine(SwitchDayNight(_isDay));
            }
        }
    }

    private IEnumerator SwitchDayNight(bool isDay)
    {
        float targetAlpha = isDay ? 0f : 0.6f;
        float startAlpha = _darkOverlay.color.a;
        float timer = 0f;

        while (timer < _fadeDuration) {
            timer += Time.deltaTime;
            float t = timer / _fadeDuration;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            _darkOverlay.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        _darkOverlay.color = new Color(0f, 0f, 0f, targetAlpha);
    }

    #endregion // coroutines
}