using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteBomb : MonoBehaviour
{
    [SerializeField] private float fuseTime = 2.5f;
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private float explosionDamage = 50f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Start()
    {
        StartCoroutine(FuseRoutine()); 
    }

    private IEnumerator FuseRoutine()
    {
        float timer = 0f;
        float blinkInterval = 0.2f;

        while (timer < 1f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        while (timer < fuseTime)
        {
            if (spriteRenderer != null)
                spriteRenderer.enabled = !spriteRenderer.enabled;

            timer += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }

        spriteRenderer.enabled = true;
        TriggerExplosion();
    }

    private void TriggerExplosion()
    {
        Debug.Log("ÆøÅº ÅÍÁü!");

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