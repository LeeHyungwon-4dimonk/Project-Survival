using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalObjects : MonoBehaviour
{
    public enum TerminalType
    {
        코팡_물류_배송장치,
        에너지_비축기,
        분해대,
        발주서_단말기,
        조종석
    }

    [SerializeField] private TerminalType _terminalType;

    public TerminalType Terminal => _terminalType;

    private static readonly Dictionary<TerminalType, string> _displayNames = new()
    {
        { TerminalType.코팡_물류_배송장치, "코팡 물류 배송장치" },
        { TerminalType.에너지_비축기,     "에너지 비축기" },
        { TerminalType.분해대,           "분해대" },
        { TerminalType.발주서_단말기,     "발주서 단말기" },
        { TerminalType.조종석,           "조종석" }
    };

    public void OnPower()
    {
        Debug.Log($"{_displayNames[_terminalType]} UI 켜기");

        switch (_terminalType)
        {
            case TerminalType.코팡_물류_배송장치:
                GameManager.Instance.InGameUIManager.ShowUI(UIType.Kopang);
                break;

            case TerminalType.에너지_비축기:
                GameManager.Instance.InGameUIManager.ShowUI(UIType.TodayReward);
                break;

            case TerminalType.분해대:
                GameManager.Instance.InGameUIManager.ShowUI(UIType.Decompose);
                break;

            case TerminalType.발주서_단말기:
                GameManager.Instance.InGameUIManager.ShowUI(UIType.Orderform);
                break;

            case TerminalType.조종석:
                // 조종석용 동작 따로 구현해놔야됨
                // GameManager.Instance.InGameUIManager.ShowUI(UIType.Cockpit);
                break;
        }
    }

    public static string GetDisplayName(TerminalType type)
    {
        return _displayNames[type];
    }

}
