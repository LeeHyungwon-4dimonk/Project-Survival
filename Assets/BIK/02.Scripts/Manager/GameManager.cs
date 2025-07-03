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
    private DayNightCycleManager _dayNightManager;

    #endregion // private fields





    #region properties

    public InGameUIManager InGameUIManager { get; set; }
    public DayNightCycleManager DayNightManager { get; set; }
    public PlayerStats PlayerStats { get; set; } = new PlayerStats();

    #endregion





    #region public funcs

    public void StartGame()
    {
        SceneManager.Instance.LoadScene("InGameScene");
    }

    public void GameOver()
    {
        SceneManager.Instance.LoadScene("MainScene");
    }

    public void SkipDay()
    {
        DayNightManager.SkipToNextDay();
    }

    #endregion // public funcs
}


public class GameData
{




    public GameData()
    {

    }
}
