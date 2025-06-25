using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField] private float _attackDamage = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerStats player = other.GetComponent<PlayerStats>();
        if (player != null)
        {
            player.TakeDamage(_attackDamage);
            StartCoroutine(HitReaction(player));
        }
    }

    private IEnumerator HitReaction(PlayerStats player)
    {
        SpriteRenderer renderer = player != null ? player.GetComponent<SpriteRenderer>() : null;
        if (renderer == null) yield break;

        for (int i = 0; i < 3; i++)
        {
            if (renderer == null) yield break;
            renderer.enabled = false;
            yield return new WaitForSeconds(0.1f);

            if (renderer == null) yield break;
            renderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        Rigidbody2D rb = player != null ? player.GetComponent<Rigidbody2D>() : null;
        if (rb != null)
        {
            Vector2 knockbackDir = (rb.position - (Vector2)transform.position).normalized;
            rb.AddForce(knockbackDir * 5f, ForceMode2D.Impulse);
        }
    }
}
