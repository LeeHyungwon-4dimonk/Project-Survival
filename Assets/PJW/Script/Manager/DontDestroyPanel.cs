using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyPanel : MonoBehaviour
{
    private static DontDestroyPanel _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        DontDestroyOnLoad(gameObject);
    }
}
