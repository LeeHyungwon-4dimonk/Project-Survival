using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderformUI : UIBase
{
    #region serialized fields

    [SerializeField] private TMP_Text _explaneText;
    [SerializeField] private TMP_Text _energyText;

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
        _explaneText.text = "TODO";
        _energyText.text = "TODO";
    }

    public override void SetHide()
    {
        base.SetHide();
    }

    public void OnClick_NextPage()
    {
        // TODO
    }

    #endregion // public funcs
}
