using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DecompositionTableUI : UIBase
{
    #region serialized fields

    [SerializeField] private TMP_Text _energyText;
    [SerializeField] private TMP_Text _weightText;

    #endregion // serialized fields





    #region mono funcs

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SetHide();
        }
    }

    #endregion // mono funcs





    #region public funcs

    public override void SetShow()
    {
        RefreshUI();
        base.SetShow();
    }

    public override void RefreshUI()
    {
        _energyText.text = "TODO";
        _weightText.text = "TODO";
    }

    public override void SetHide()
    {
        base.SetHide();
    }

    public void OnClick_StorageAll()
    {
        // TODO
    }

    public void OnClick_EjectAll()
    {
        // TODO
    }

    public void OnClick_Decomposition()
    {
        // TODO
    }

    #endregion // public funcs
}
