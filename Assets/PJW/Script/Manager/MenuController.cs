using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject menuPopup;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject quitConfirmPanel;

    [Header("Buttons")]
    [SerializeField] private Button btnBack;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnQuit;
    [SerializeField] private Button btnOk;     
    [SerializeField] private Button btnCancel; 

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        btnBack.onClick.AddListener(OnBack);
        btnSettings.onClick.AddListener(OnSettings);
        btnQuit.onClick.AddListener(OnQuit);

        btnOk.onClick.AddListener(OnConfirmQuit);
        btnCancel.onClick.AddListener(OnCancelQuit);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuPopup != null)
                menuPopup.SetActive(!menuPopup.activeSelf);
        }
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
        if (quitConfirmPanel != null)
            quitConfirmPanel.SetActive(true); 
    }

    private void OnConfirmQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnCancelQuit()
    {
        if (quitConfirmPanel != null)
            quitConfirmPanel.SetActive(false); 
    }
}
