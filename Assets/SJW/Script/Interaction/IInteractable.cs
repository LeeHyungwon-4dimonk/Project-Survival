using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    // Ÿ�Ժз�
    enum InteractionType { };

    // �÷��̾ ������ ���� �� UI �� ǥ���� �ؽ�Ʈ (��: "E Ȯ���ϱ�")
    string GetDescription();

    // ��ȣ�ۿ� ���� ����
    void Interact();
}
