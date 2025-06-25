using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    // �÷��̾ ������ ���� �� UI �� ǥ���� �ؽ�Ʈ (��: "E Ȯ���ϱ�")
    string GetDescription();

    // ��ȣ�ۿ뿡 ����� Ű (��: KeyCode.E)
    KeyCode GetKey();

    // ��ȣ�ۿ� ���� ����
    void Interact();
}
