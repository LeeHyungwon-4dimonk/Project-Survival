using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [Header("SFX Settings")]
    [SerializeField] private AudioSource audioSource;   
    [SerializeField] private AudioClip clickClip;      
    [SerializeField, Range(0f,1f)] private float volume = 1f;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PlayClick);
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }

    private void PlayClick()
    {
        if (clickClip != null && audioSource != null)
            audioSource.PlayOneShot(clickClip, volume);
    }

    public void SetVolume(float newVolume)
    {
        volume = newVolume;
    }
}
