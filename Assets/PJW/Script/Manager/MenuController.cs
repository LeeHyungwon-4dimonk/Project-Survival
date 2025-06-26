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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuPopup != null)
                menuPopup.SetActive(!menuPopup.activeSelf);
        }
    }

    // BackButton
    public void OnBack()
    {
        if (menuPopup != null)
            menuPopup.SetActive(false);
    }

    // OptionButton
    public void OnSettings()
    {
        if (optionPanel != null)
            optionPanel.SetActive(true);
    }

    // MenuQuit Button
    public void OnQuit()
    {
        if (quitConfirmPanel != null)
            quitConfirmPanel.SetActive(true);
    }

    // QuitPanel - OkButton
    public void OnConfirmQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#else
        Application.Quit(); 
#endif
    }

    // QuitPanel - QuitButton
    public void OnCancelQuit()
    {
        if (quitConfirmPanel != null)
            quitConfirmPanel.SetActive(false);
    }
}
