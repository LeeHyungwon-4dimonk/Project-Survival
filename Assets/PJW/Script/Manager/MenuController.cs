using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject menuPopup;
    [SerializeField] private GameObject optionPanel;

    [Header("Buttons")]
    [SerializeField] private Button btnBack;      
    [SerializeField] private Button btnSettings;  
    [SerializeField] private Button btnQuit;      

    private void Awake()
    {
        btnBack.onClick.AddListener(OnBack);
        btnSettings.onClick.AddListener(OnSettings);
        btnQuit.onClick.AddListener(OnQuit);
    }

    private void OnBack()
    {
        if (menuPopup != null)
            menuPopup.SetActive(false);
    }

    private void OnSettings()
    {
        if (optionPanel != null)
            optionPanel.SetActive(true);
    }

    private void OnQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
