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
            c.a = 0f; // ���İ��� 0���� ����
            img.color = c;
        }
    }
}
