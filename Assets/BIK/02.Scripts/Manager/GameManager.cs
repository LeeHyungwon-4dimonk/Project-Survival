using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion // Singleton





    #region private fields

    private InGameUIManager _inGameUIManager;

    #endregion // private fields





    #region properties

    public InGameUIManager InGameUIManager { get; set; }

    #endregion





    #region public funcs

    public void StartGame()
    {
        SceneManager.Instance.LoadScene("InGameScene");
    }

    #endregion // public funcs
}


public class GameData
{




    public GameData()
    {

    }
}
