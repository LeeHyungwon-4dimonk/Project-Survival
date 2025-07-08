using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathPanelController : MonoBehaviour
{
    public static DeathPanelController Instance { get; private set; }

    [Header("UI Panels")]
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject quitPanel;

    [Header("Death Panel Buttons")]
    [SerializeField] private Button deathOkButton;
    [SerializeField] private Button deathCancelButton;

    [Header("Quit Panel Buttons")]
    [SerializeField] private Button quitOkButton;
    [SerializeField] private Button quitCancelButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (deathPanel != null) deathPanel.SetActive(false);
        if (quitPanel != null)  quitPanel.SetActive(false);

        if (deathOkButton != null) deathOkButton.onClick.AddListener(OnDeathOk);
        if (deathCancelButton != null) deathCancelButton.onClick.AddListener(OnDeathCancel);
        if (quitOkButton != null) quitOkButton.onClick.AddListener(OnQuitOk);
        if (quitCancelButton != null) quitCancelButton.onClick.AddListener(OnQuitCancel);
    }

    public void ShowDeathPanel()
    {
        if (deathPanel == null) return;
        deathPanel.SetActive(true);
        Time.timeScale = 0f; 
    }
    private void OnDeathOk()
    {
        if (deathPanel != null) 
        deathPanel.SetActive(false);
        
        Time.timeScale = 1f; 
        SceneManager.Instance.LoadScene("MainScene"); 
    }

    private void OnDeathCancel()
    {
        if (quitPanel == null) return;
        quitPanel.SetActive(true);
    }

    private void OnQuitOk()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    
    private void OnQuitCancel()
    {
        if (quitPanel == null) return;
        quitPanel.SetActive(false);
    }
}

