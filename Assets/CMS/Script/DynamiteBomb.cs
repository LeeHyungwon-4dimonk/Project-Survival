using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteBomb : MonoBehaviour
{
    [SerializeField] private float fuseTime = 2.5f;
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private float explosionDamage = 50f;
    [SerializeField] private LayerMask targetLayer;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(FuseRoutine());
    }

    private IEnumerator FuseRoutine()
    {
        yield return new WaitForSeconds(fuseTime);

        GetComponent<Animator>()?.SetTrigger("Explode");
    }
    public void TriggerExplosion()
    {
        Debug.Log("다이너마이트 폭발!");

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, targetLayer);
        foreach (var hit in hits)
        {
            MonsterStats monster = hit.GetComponent<MonsterStats>();
            if (monster != null)
                monster.TakeDamage(explosionDamage);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}