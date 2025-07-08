using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _dynamiteThrowPrefab;
    [SerializeField] private float _throwForce = 5f;
    [SerializeField] private ItemSO _dynamiteItemSO;

    private Animator _animator;
    private bool _isThrowing = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !_isThrowing)
        {
            TryThrowDynamite();
        }
    }

    private void TryThrowDynamite()
    {
        if (InventoryManager.Instance == null)
        {
            Debug.LogError("InventoryManager 인스턴스가 없습니다.");
            return;
        }

        if (!InventoryManager.Instance.RemoveItemFromInventory(_dynamiteItemSO, 1))
        {
            Debug.Log("인벤토리에 다이너마이트가 없습니다.");
            return;
        }

        StartCoroutine(ThrowDynamiteRoutine());
    }

    private IEnumerator ThrowDynamiteRoutine()
    {
        _isThrowing = true;
        _animator.SetBool("IsThrowing", true);

        yield return new WaitForSeconds(0.2f);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector2 dir = (mousePos - transform.position).normalized;

        if (dir == Vector2.zero)
            dir = Vector2.right;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            _animator.SetFloat("ThrowX", dir.x > 0 ? 1 : -1);
            _animator.SetFloat("ThrowY", 0);
        }
        else
        {
            _animator.SetFloat("ThrowX", 0);
            _animator.SetFloat("ThrowY", dir.y > 0 ? 1 : -1);
        }

        Vector3 spawnPos = transform.position + (Vector3)(dir * 0.5f);
        GameObject bomb = Instantiate(_dynamiteThrowPrefab, spawnPos, Quaternion.identity);

        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D bombCollider = bomb.GetComponent<Collider2D>();

        if (playerCollider != null && bombCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, bombCollider);
        }

        Physics2D.IgnoreCollision(bomb.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(dir * _throwForce, ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(0.4f);
        _animator.SetBool("IsThrowing", false);
        _isThrowing = false;
    }
}

