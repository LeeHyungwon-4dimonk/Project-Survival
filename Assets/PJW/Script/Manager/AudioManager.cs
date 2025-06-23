using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager Instance { get; private set; }
    #endregion

    #region Inspector Fields
    [SerializeField] private AudioSource _bgmSource;
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

        if (_bgmSource == null) _bgmSource = gameObject.AddComponent<AudioSource>();
        _bgmSource.loop = true;

        float savedVolume = PlayerPrefs.GetFloat("BgmVolume", 1f);
        BgmVolume = savedVolume;
    }
    #endregion

    #region Properties
    public float BgmVolume
    {
        get => _bgmSource.volume;
        set
        {
            _bgmSource.volume = Mathf.Clamp01(value);
            PlayerPrefs.SetFloat("BgmVolume", _bgmSource.volume);
            PlayerPrefs.Save();
        }
    }
    #endregion

    #region Public Methods
    public void PlayBgm(AudioClip clip)
    {
        if (_bgmSource.clip == clip) return;
        _bgmSource.clip = clip;
        _bgmSource.Play();
    }

    public void StopBgm()
    {
        _bgmSource.Stop();
    }
    #endregion
}
