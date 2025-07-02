using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneUIController : MonoBehaviour
{
    #region public funcs

    public void OnClick_GameStart()
    {
        GameManager.Instance.StartGame();
    }

    public void OnClick_Exit()
    {
        // TODO
    }

    public void OnClick_Option()
    {
        MenuController.Instance.ShowOption();
    }

    #endregion // public funcs
}
