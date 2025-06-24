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
    #endregion

    #region Constants
    private const string MixerParamBgm = "BgmVolume";
    private const string MixerParamSfx = "SfxVolume";

    private const string PrefBgm = "Pref_BgmVolume";
    private const string PrefSfx = "Pref_SfxVolume";
    #endregion

    #region Private Fields
    private AudioSource _bgmSource;
    private AudioSource _sfxSource;
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

        _bgmSource = gameObject.AddComponent<AudioSource>();
        _bgmSource.loop = true;
        _bgmSource.outputAudioMixerGroup = _bgmGroup;

        _sfxSource = gameObject.AddComponent<AudioSource>();
        _sfxSource.loop = false;
        _sfxSource.outputAudioMixerGroup = _sfxGroup;
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
        if (clip == null) return;
        if (_bgmSource.clip == clip && _bgmSource.isPlaying) return;
        _bgmSource.clip = clip;
        _bgmSource.Play();
    }

    public void StopBgm()
    {
        _bgmSource.Stop();
    }

    public void PlaySfx(AudioClip clip)
    {
        if (clip == null) return;
        _sfxSource.PlayOneShot(clip);
    }
    #endregion
}
