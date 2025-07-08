using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CollectionPanelController : MonoBehaviour
{
    [SerializeField] private GameObject collectionPanel;
    [Header("Sounds")]
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;

    private bool isOpen = false;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }

    private void Start()
    {
        if (collectionPanel != null)
            collectionPanel.SetActive(isOpen);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ToggleCollectionPanel();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && isOpen)
        {
            CloseCollectionPanel();
        }
        
    }

    private void ToggleCollectionPanel()
    {
        if (collectionPanel == null) return;

        isOpen = !isOpen;
        collectionPanel.SetActive(isOpen);

        if (isOpen)
            PlaySound(openClip);
        else
            PlaySound(closeClip);
    }
    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;
        _audioSource.PlayOneShot(clip);
    }

    private void CloseCollectionPanel()
    {
        if (collectionPanel == null) return;

        isOpen = false;
        collectionPanel.SetActive(false);
    }
}
