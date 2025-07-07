using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalObjects : MonoBehaviour
{
    public enum TerminalType
    {
        ����_����_�����ġ,
        ������_�����,
        ���ش�,
        ���ּ�_�ܸ���,
        ������
    }

    [SerializeField] private TerminalType _terminalType;

    public TerminalType Terminal => _terminalType;

    private static readonly Dictionary<TerminalType, string> _displayNames = new()
    {
        { TerminalType.����_����_�����ġ, "���� ���� �����ġ" },
        { TerminalType.������_�����,     "������ �����" },
        { TerminalType.���ش�,           "���ش�" },
        { TerminalType.���ּ�_�ܸ���,     "���ּ� �ܸ���" },
        { TerminalType.������,           "������" }
    };

    public void OnPower()
    {
        Debug.Log($"{_displayNames[_terminalType]} UI �ѱ�");

        switch (_terminalType)
        {
            case TerminalType.����_����_�����ġ:
                GameManager.Instance.InGameUIManager.ShowUI(UIType.Kopang);
                break;

            case TerminalType.������_�����:
                GameManager.Instance.InGameUIManager.ShowUI(UIType.TodayReward);
                break;

            case TerminalType.���ش�:
                GameManager.Instance.InGameUIManager.ShowUI(UIType.Decompose);
                break;

            case TerminalType.���ּ�_�ܸ���:
                GameManager.Instance.InGameUIManager.ShowUI(UIType.Orderform);
                break;

            case TerminalType.������:
                // �������� ���� ���� �����س��ߵ�
                // GameManager.Instance.InGameUIManager.ShowUI(UIType.Cockpit);
                break;
        }
    }

    public static string GetDisplayName(TerminalType type)
    {
        return _displayNames[type];
    }

}
