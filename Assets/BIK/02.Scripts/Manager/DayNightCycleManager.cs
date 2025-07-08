using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
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
    private bool _isNightOvertime = false;
    private bool _isPaused = false;
    private bool _isInBase = false;

    #endregion





    #region Properties

    public int CurrentDay => _currentDay;
    public bool IsDayTime => _isDay;
    public bool IsNightOvertime => _isNightOvertime;
    public bool IsInBase => _isInBase;

    #endregion





    #region Serialized Fields

    [SerializeField] private Light2D _globalLight;
    [SerializeField] private float _fadeDuration = 2f;
    [SerializeField] private float _dayIntensity = 1f;
    [SerializeField] private float _nightIntensity = 0.3f;

    #endregion





    #region MonoBehaviour

    private void Start()
    {
        GameManager.Instance.DayNightManager = this;
        EnterBase();
        StartCoroutine(DayNightCycleRoutine());
    }

    #endregion





    #region Coroutines

    private IEnumerator DayNightCycleRoutine()
    {
        while (true) {
            yield return new WaitForSeconds(1f);

            if (_isPaused)
                continue;

            _timePassed += 1f;

            if (_timePassed >= TotalDayDuration) {
                _timePassed -= TotalDayDuration;
                _currentDay++;
                _isNightOvertime = false;
            }

            bool isNowDay = _timePassed < DayDuration;
            if (_isDay != isNowDay) {
                _isDay = isNowDay;
                StartCoroutine(SwitchDayNight(_isDay));
            }

            if (!_isDay && _timePassed >= DayDuration + NightDuration) {
                _isNightOvertime = true;

                if (!_isInBase) {
                    // TODO : Player Damage
                }
            }
        }
    }


    private IEnumerator SwitchDayNight(bool isDay)
    {
        float startIntensity = _globalLight.intensity;
        float targetIntensity = isDay ? _dayIntensity : _nightIntensity;
        float timer = 0f;

        while (timer < _fadeDuration) {
            timer += Time.deltaTime;
            float t = timer / _fadeDuration;
            _globalLight.intensity = Mathf.Lerp(startIntensity, targetIntensity, t);
            yield return null;
        }

        _globalLight.intensity = targetIntensity;
    }

    #endregion





    #region Public funcs

    public void EnterBase()
    {
        _isPaused = true;
        _isInBase = true;
    }

    public void ExitBase()
    {
        _isPaused = false;
        _isInBase = false;
    }

    public void SkipToNextDay()
    {
        _currentDay++;
        _timePassed = 0f;
        _isDay = true;
        _isNightOvertime = false;
        StartCoroutine(SwitchDayNight(true));
    }

    #endregion // public funcs
}
