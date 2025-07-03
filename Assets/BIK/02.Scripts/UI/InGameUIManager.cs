using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    Story = 0,
    Kopang,
    Decompose,
    Orderform,
    TodayReward,
    Crafting,
    Box,
}

public class InGameUIManager : MonoBehaviour
{
    #region serialized fields

    [SerializeField] private UIBase[] _uis;

    #endregion // serialized fields




    #region private fields

    private Stack<UIBase> _uiStack = new Stack<UIBase>();

    #endregion // private fields





    #region mono funcs

    private void Awake()
    {
        // Initialize this to GameManager
        GameManager.Instance.InGameUIManager = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (_uiStack.Count > 0) {
                HideUI();
            }
            else {
                // TODO Open Option to Here PLZ PJW
            }
        }
    }

    #endregion // mono funcs





    #region public funcs

    /// <summary>
    /// Show UIBase To Stack
    /// </summary>
    /// <param name="uiType">PLZ Check enum UIType</param>
    public void ShowUI(UIType uiType)
    {
        GameManager.Instance.IsUIOpen = true;

        if (_uiStack.Count > 0) {
            _uiStack.Peek().SetHide();
        }

        var ui = _uis[(int)uiType];
        ui.SetShow();
        _uiStack.Push(ui);
    }

    public void HideUI()
    {
        if (_uiStack.Count == 0) {
            return;
        }

        var topUI = _uiStack.Pop();
        topUI.SetHide();

        if (_uiStack.Count > 0) {
            _uiStack.Peek().SetShow();
        }

        if (_uiStack.Count == 0) {
            GameManager.Instance.IsUIOpen = false;
        }
    }

    #endregion // public funcs
}