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

    private Stack<GameObject> _panelStack = new Stack<GameObject>();

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
        if (backButton != null)       backButton.onClick.AddListener(OnBack);
        if (settingsButton != null)   settingsButton.onClick.AddListener(OnSettings);
        if (quitButton != null)       quitButton.onClick.AddListener(OnQuit);
        if (confirmQuitButton != null) confirmQuitButton.onClick.AddListener(OnConfirmQuit);
        if (cancelQuitButton != null)  cancelQuitButton.onClick.AddListener(OnCancelQuit);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            HandleEscape();
    }

    private void HandleEscape()
    {
        if (_panelStack.Count > 0)
        {
            // 이미 열린 창이 있으면 가장 마지막에 열린 창부터 닫는다
            HideTopPanel();
        }
        else
        {
            // 아무 창도 열려 있지 않으면 메인 메뉴를 연다
            ShowPanel(menuPopup);
        }
    }

    private void ShowPanel(GameObject panel)
    {
        if (panel == null) return;
        panel.SetActive(true);
        _panelStack.Push(panel);
    }

    private void HideTopPanel()
    {
        if (_panelStack.Count == 0) return;
        var top = _panelStack.Pop();
        top.SetActive(false);
    }

    // 메뉴팝업의 Back 버튼 (또는 외부에서 호출)
    public void OnBack()
    {
        HideTopPanel();
    }

    // Settings 버튼
    public void OnSettings()
    {
        ShowPanel(optionPanel);
    }

    // Quit 버튼
    public void OnQuit()
    {
        ShowPanel(quitConfirmPanel);
    }

    // Quit 확인
    public void OnConfirmQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Quit 취소
    public void OnCancelQuit()
    {
        HideTopPanel();
    }
}
