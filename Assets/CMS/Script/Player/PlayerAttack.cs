using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _dynamitePrefab;
    [SerializeField] private Transform _throwPoint;

    private Animator _animator;
    private bool _isThrowing = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !_isThrowing && _dynamitePrefab != null)
        {
            StartCoroutine(ThrowDynamiteCoroutine(_dynamitePrefab));
        }
    }

    private IEnumerator ThrowDynamiteCoroutine(GameObject prefab)
    {
        _isThrowing = true;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 dir = (mousePos - transform.position).normalized;

        _animator.SetFloat("ThrowX", dir.x);
        _animator.SetFloat("ThrowY", dir.y);
        _animator.SetBool("IsThrowing", true);

        yield return new WaitForSeconds(0.3f);

        _animator.SetBool("IsThrowing", false);
        _isThrowing = false;

        float maxThrowDistance = 8f;
        Vector2 throwTarget = (Vector2)transform.position + dir * Mathf.Min(Vector2.Distance(transform.position, mousePos), maxThrowDistance);

        Vector3 spawnPos = _throwPoint != null
            ? _throwPoint.position
            : transform.position + (Vector3)(dir * 0.5f); 

        GameObject bomb = Instantiate(prefab, spawnPos, Quaternion.identity);

        Collider2D[] playerColliders = GetComponentsInChildren<Collider2D>();
        Collider2D bombCollider = bomb.GetComponent<Collider2D>();
        foreach (var col in playerColliders)
            Physics2D.IgnoreCollision(bombCollider, col);

        Vector2 throwDir = (throwTarget - (Vector2)spawnPos).normalized;

        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(throwDir * 10f, ForceMode2D.Impulse);
        }
    }
}

