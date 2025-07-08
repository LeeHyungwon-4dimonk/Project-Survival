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

    [Header("Quick Menu")]
    [SerializeField] private GameObject quickMenuPanel;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quickMenuOkButton;
    [SerializeField] private Button quickMenuCancelButton;

    [Header("Buttons")]
    [SerializeField] private Button backButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button confirmQuitButton;
    [SerializeField] private Button cancelQuitButton;

    private Stack<GameObject> _panelStack = new Stack<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (backButton != null)
            backButton.onClick.AddListener(OnBack);
        if (settingsButton != null)
            settingsButton.onClick.AddListener(OnSettings);
        if (quitButton != null)
            quitButton.onClick.AddListener(OnQuit);
        if (confirmQuitButton != null)
            confirmQuitButton.onClick.AddListener(OnConfirmQuit);
        if (cancelQuitButton != null)
            cancelQuitButton.onClick.AddListener(OnCancelQuit);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(OnMainMenuButton);
        if (quickMenuOkButton != null)
            quickMenuOkButton.onClick.AddListener(OnQuickMenuOk);
        if (quickMenuCancelButton != null)
            quickMenuCancelButton.onClick.AddListener(OnQuickMenuCancel);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //    HandleEscape();
    }

    public void HandleEscape()
    {
        if (_panelStack.Count > 0) {
            HideTopPanel();
        }
        else {
            ShowPanel(menuPopup);
        }
    }

    private void ShowPanel(GameObject panel)
    {
        if (panel == null)
            return;
        panel.SetActive(true);
        _panelStack.Push(panel);
    }

    private void HideTopPanel()
    {
        if (_panelStack.Count == 0)
            return;
        var top = _panelStack.Pop();
        top.SetActive(false);
    }

    public void OnBack() => HideTopPanel();
    public void OnSettings() => ShowPanel(optionPanel);
    public void OnQuit() => ShowPanel(quitConfirmPanel);
    public void OnConfirmQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void OnCancelQuit() => HideTopPanel();

    private void OnMainMenuButton()
    {
        ShowPanel(quickMenuPanel);
    }

    private void OnQuickMenuOk()
    {
        HideTopPanel();
        // 2) 메인 메뉴 패널 닫기
        HideTopPanel();
        SceneManager.Instance.LoadScene("MainScene");
    }

    private void OnQuickMenuCancel()
    {
        HideTopPanel();
    }
}
