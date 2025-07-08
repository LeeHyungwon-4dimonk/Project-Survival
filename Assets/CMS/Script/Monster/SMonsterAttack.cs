using System.Collections;
using UnityEngine;

public class SMonsterAttack : MonoBehaviour
{
    [SerializeField] private float _attackDamage = 5f;
    [SerializeField] private float _knockbackForce = 5f;
    [SerializeField] private float _hitCooldown = 1f;
    [SerializeField] private float _monsterPauseTime = 0.3f;

    private Rigidbody2D _monsterRb;
    private bool _canAttack = true;
    private PlayerStats _playerInRange;

    private void Awake()
    {
        _monsterRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_playerInRange != null && _canAttack)
        {
            StartCoroutine(AttackRoutine(_playerInRange));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats player = other.GetComponent<PlayerStats>();
            if (player != null)
            {
                _playerInRange = player;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats player = other.GetComponent<PlayerStats>();
            if (_playerInRange == player)
            {
                _playerInRange = null;
            }
        }
    }

    private IEnumerator AttackRoutine(PlayerStats player)
    {
        _canAttack = false;

        if (_monsterRb != null)
            _monsterRb.velocity = Vector2.zero;

        player.TakeDamage(_attackDamage);

        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            Vector2 knockbackDir = (playerRb.position - (Vector2)transform.position).normalized;
            yield return ApplyKnockback(playerRb, knockbackDir, _knockbackForce, 0.15f);
        }

        yield return new WaitForSeconds(_monsterPauseTime + _hitCooldown);

        _canAttack = true;
    }

    private IEnumerator ApplyKnockback(Rigidbody2D rb, Vector2 direction, float force, float stopDelay)
    {
        if (rb == null) yield break;

        rb.velocity = Vector2.zero;
        rb.AddForce(direction * force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(stopDelay);

        rb.velocity = Vector2.zero;
    }
}

