using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
   #region Singleton
    public static AudioManager Instance { get; private set; }
    #endregion

    #region Inspector Fields
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioMixerGroup _bgmGroup;
    [SerializeField] private AudioMixerGroup _sfxGroup;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioSource _sfxSource;
    #endregion

    #region Constants
    private const string MixerParamBgm = "BgmVolume";
    private const string MixerParamSfx = "SfxVolume";

    private const string PrefBgm = "Pref_BgmVolume";
    private const string PrefSfx = "Pref_SfxVolume";
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (_bgmSource != null)
        {
            _bgmSource.loop = true;
            _bgmSource.outputAudioMixerGroup = _bgmGroup;
            _bgmSource.playOnAwake = false;
        }

        if (_sfxSource != null)
        {
            _sfxSource.loop = false;
            _sfxSource.outputAudioMixerGroup = _sfxGroup;
            _sfxSource.playOnAwake = false;
        }
    }

    private void Start()
    {
        float savedBgm = PlayerPrefs.GetFloat(PrefBgm, 1f);
        float savedSfx = PlayerPrefs.GetFloat(PrefSfx, 1f);
        SetBgmVolume(savedBgm);
        SetSfxVolume(savedSfx);
    }
    #endregion

    #region Public Volume API
    public void SetBgmVolume(float linear)
    {
        float clamped = Mathf.Clamp(linear, 0.0001f, 1f);
        float dB = Mathf.Log10(clamped) * 20f;
        _audioMixer.SetFloat(MixerParamBgm, dB);
        PlayerPrefs.SetFloat(PrefBgm, clamped);
        PlayerPrefs.Save();
    }

    public void SetSfxVolume(float linear)
    {
        float clamped = Mathf.Clamp(linear, 0.0001f, 1f);
        float dB = Mathf.Log10(clamped) * 20f;
        _audioMixer.SetFloat(MixerParamSfx, dB);
        PlayerPrefs.SetFloat(PrefSfx, clamped);
        PlayerPrefs.Save();
    }
    #endregion

    #region Public Playback Methods
    public void PlayBgm(AudioClip clip)
    {
        if (_bgmSource == null || clip == null) return;
        if (_bgmSource.clip == clip && _bgmSource.isPlaying) return;
        _bgmSource.clip = clip;
        _bgmSource.Play();
    }

    public void StopBgm()
    {
        if (_bgmSource == null) return;
        _bgmSource.Stop();
    }

    public void PlaySfx(AudioClip clip)
    {
        if (_sfxSource == null || clip == null) return;
        _sfxSource.PlayOneShot(clip);
    }
    #endregion
}
