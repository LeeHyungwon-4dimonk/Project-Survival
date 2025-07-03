using System.Collections;
using UnityEngine;

public class SMonsterAttack : MonoBehaviour
{
    [SerializeField] private float _attackDamage = 5f;
    [SerializeField] private float _knockbackForce = 5f;
    [SerializeField] private float _hitCooldown = 1f;
    [SerializeField] private float _monsterPauseTime = 0.3f;

    private Rigidbody2D _monsterRb;
    private Coroutine _attackCoroutine;

    private void Awake()
    {
        _monsterRb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _attackCoroutine == null)
        {
            PlayerStats player = other.GetComponent<PlayerStats>();
            if (player != null)
            {
                _attackCoroutine = StartCoroutine(AttackLoop(player));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
    }

    private IEnumerator AttackLoop(PlayerStats player)
    {
        while (player != null)
        {
            if (_monsterRb != null)
                _monsterRb.velocity = Vector2.zero;

            player.TakeDamage(_attackDamage);

            SpriteRenderer renderer = player.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                for (int i = 0; i < 2; i++)
                {
                    renderer.enabled = false;
                    yield return new WaitForSeconds(0.05f);
                    renderer.enabled = true;
                    yield return new WaitForSeconds(0.05f);
                }
            }

            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 knockbackDir = (playerRb.position - (Vector2)transform.position).normalized;
                playerRb.velocity = Vector2.zero;
                playerRb.AddForce(knockbackDir * _knockbackForce, ForceMode2D.Impulse);

                yield return new WaitForSeconds(0.1f); 
                playerRb.velocity = Vector2.zero;
            }

            yield return new WaitForSeconds(_monsterPauseTime);

            yield return new WaitForSeconds(_hitCooldown);
        }

        _attackCoroutine = null;
    }
    private IEnumerator StopPlayerKnockback(Rigidbody2D rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (rb != null)
            rb.velocity = Vector2.zero;
    }


}
