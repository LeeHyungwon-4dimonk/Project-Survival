using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Button _okButton;
    [SerializeField] private Button _cancelButton;

    [Header("Panels")]
    [SerializeField] private GameObject _optionPanel;

    private const string PREF_BGM = "Pref_BgmVolume";
    private const string PREF_SFX = "Pref_SfxVolume";

    private float _initialBgm;
    private float _initialSfx;

    private bool _isInitialized = false;

    private void Awake()
    {
        _okButton.onClick.AddListener(OnOk);
        _cancelButton.onClick.AddListener(OnCancel);
    }

    private void OnEnable()
    {
        if (!_isInitialized)
        {
            float savedBgm = PlayerPrefs.GetFloat(PREF_BGM, 1f);
            float savedSfx = PlayerPrefs.GetFloat(PREF_SFX, 1f);

            _bgmSlider.SetValueWithoutNotify(savedBgm);
            _sfxSlider.SetValueWithoutNotify(savedSfx);

            _bgmSlider.onValueChanged.AddListener(AudioManager.Instance.SetBgmVolume);
            _sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSfxVolume);

            _isInitialized = true;
        }

        _initialBgm = _bgmSlider.value;
        _initialSfx = _sfxSlider.value;
    }

    private void OnOk()
    {
        _optionPanel.SetActive(false);
    }

    private void OnCancel()
    {
        _bgmSlider.SetValueWithoutNotify(_initialBgm);
        _sfxSlider.SetValueWithoutNotify(_initialSfx);
        AudioManager.Instance.SetBgmVolume(_initialBgm);
        AudioManager.Instance.SetSfxVolume(_initialSfx);

        _optionPanel.SetActive(false);
    }
}
