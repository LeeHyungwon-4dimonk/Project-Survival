using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryUI : UIBase
{
    #region serialized fields

    [SerializeField] private TMP_Text _storyText;

    #endregion // serialized fields





    #region public funcs

    public override void SetShow()
    {
        RefreshUI();
        base.SetShow();
    }

    public override void RefreshUI()
    {
        _storyText.text = "TODO";
    }

    public override void SetHide()
    {
        base.SetHide();
    }

    public void OnClick_Panel()
    {
        SetHide();
    }

    #endregion // public funcs
}
