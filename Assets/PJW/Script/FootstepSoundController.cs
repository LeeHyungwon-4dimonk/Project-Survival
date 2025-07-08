using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepSoundController : MonoBehaviour
{
    [SerializeField] private AudioClip[] footstepClips;
    [SerializeField] private PlayerMovement playerMovement;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;

        if (playerMovement == null)
            playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerMovement.MoveInput.sqrMagnitude > 0.01f)
        {
            if (!_audioSource.isPlaying && footstepClips.Length > 0)
            {
                _audioSource.clip = footstepClips[Random.Range(0, footstepClips.Length)];
                _audioSource.Play();
            }
        }
        else
        {
            if (_audioSource.isPlaying)
                _audioSource.Stop();
        }
    }
}
