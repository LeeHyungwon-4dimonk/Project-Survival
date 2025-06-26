using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
   #region Singleton
    public static MenuController Instance { get; private set; }
    #endregion

    [Header("Panels")]
    [SerializeField] private GameObject menuPopup;
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject quitConfirmPanel;

    [Header("Buttons")]
    [SerializeField] private Button backButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button confirmQuitButton;
    [SerializeField] private Button cancelQuitButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // 버튼 클릭 이벤트 연결
        if (backButton != null) backButton.onClick.AddListener(OnBack);
        if (settingsButton != null) settingsButton.onClick.AddListener(OnSettings);
        if (quitButton != null) quitButton.onClick.AddListener(OnQuit);
        if (confirmQuitButton != null) confirmQuitButton.onClick.AddListener(OnConfirmQuit);
        if (cancelQuitButton != null) cancelQuitButton.onClick.AddListener(OnCancelQuit);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenuPopup();
        }
    }

    // ESC키 또는 외부 호출로 메뉴 팝업 토글
    public void ToggleMenuPopup()
    {
        if (menuPopup != null)
        {
            menuPopup.SetActive(!menuPopup.activeSelf);
        }
    }

    // 외부에서 옵션 패널 보여주기
    public void ShowOption()
    {
        if (optionPanel != null)
        {
            optionPanel.SetActive(true);
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
        ShowOption();
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

    // QuitPanel - CancelButton
    public void OnCancelQuit()
    {
        if (quitConfirmPanel != null)
            quitConfirmPanel.SetActive(false);
    }
}
