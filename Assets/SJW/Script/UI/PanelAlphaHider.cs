using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelAlphaHider : MonoBehaviour
{
    void Start()
    {
        Image img = GetComponent<Image>();
        if (img != null)
        {
            Color c = img.color;
            c.a = 0f; // 알파값만 0으로 조정
            img.color = c;
        }
    }
}
