using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TodayRewardUI : UIBase
{
    #region serialized fields

    [SerializeField] private TMP_Text _explaneText;

    #endregion // serialized fields





    #region mono funcs



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
    }

    public override void SetHide()
    {
        base.SetHide();
    }

    public void OnClick_GetReward()
    {
        // TODO
    }

    public void OnClick_Close()
    {
        SetHide();
    }

    #endregion // public funcs
}
