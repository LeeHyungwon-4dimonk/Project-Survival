using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private string sceneName = "";

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClickLoadScene);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClickLoadScene);
    }

    private void OnClickLoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }

    internal static void LoadScene(string v)
    {
    
    }
}
