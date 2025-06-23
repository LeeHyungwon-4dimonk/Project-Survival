using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    #region Inspector Fields
    [SerializeField] private Slider _volumeSlider;
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        _volumeSlider.value = AudioManager.Instance.BgmVolume;
        _volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }
    #endregion

    #region Private Methods
    private void OnVolumeChanged(float value)
    {
        AudioManager.Instance.BgmVolume = value;
    }
    #endregion
}
