using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[DefaultExecutionOrder(-100)]
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
    #endregion

    #region Private Fields
    private AudioSource _bgmSource;
    private AudioSource _sfxSource;
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

        AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if (source.outputAudioMixerGroup == _bgmGroup)
            {
                _bgmSource = source;
            }
            else if (source.outputAudioMixerGroup == _sfxGroup)
            {
                _sfxSource = source;
            }
        }

        if (_bgmSource != null)
        {
            _bgmSource.loop = true;
            _bgmSource.playOnAwake = false;
        }

        if (_sfxSource != null)
        {
            _sfxSource.loop = false;
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

    #region Public Properties
    public float BgmVolume
    {
        get => _bgmSource != null ? _bgmSource.volume : 0f;
        private set
        {
            if (_bgmSource != null)
                _bgmSource.volume = Mathf.Clamp01(value);
        }
    }

    public float SfxVolume
    {
        get => _sfxSource != null ? _sfxSource.volume : 0f;
        private set
        {
            if (_sfxSource != null)
                _sfxSource.volume = Mathf.Clamp01(value);
        }
    }
    #endregion

    #region Public Volume Methods
    public void SetBgmVolume(float linear)
    {
        float clamped = Mathf.Clamp(linear, 0.0001f, 1f);
        float dB = Mathf.Log10(clamped) * 20f;

        if (_audioMixer != null)
            _audioMixer.SetFloat(MixerParamBgm, dB);

        PlayerPrefs.SetFloat(PrefBgm, clamped);
        PlayerPrefs.Save();
    }

    public void SetSfxVolume(float linear)
    {
        float clamped = Mathf.Clamp(linear, 0.0001f, 1f);
        float dB = Mathf.Log10(clamped) * 20f;

        if (_audioMixer != null)
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
        if (_bgmSource != null)
            _bgmSource.Stop();
    }

    public void PlaySfx(AudioClip clip)
    {
        if (_sfxSource == null || clip == null) return;
        _sfxSource.PlayOneShot(clip);
    }
    #endregion
}
