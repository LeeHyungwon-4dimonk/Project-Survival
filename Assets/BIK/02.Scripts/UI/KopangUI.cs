using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KopangUI : UIBase
{
    #region serialized fields



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

    }

    public override void SetHide()
    {
        base.SetHide();
    }

    public void OnClick_Launch()
    {
        // TODO
    }

    #endregion // public funcs
}
