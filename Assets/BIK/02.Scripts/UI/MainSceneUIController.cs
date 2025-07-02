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
        MenuController.Instance.OnQuit();
    }

    public void OnClick_Option()
    {
        MenuController.Instance.OnSettings();
    }

    #endregion // public funcs
}
