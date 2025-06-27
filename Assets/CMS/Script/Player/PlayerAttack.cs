using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _attackDamage = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        MonsterStats monster = other.GetComponent<MonsterStats>();
        if (monster != null)
        {
            monster.TakeDamage(_attackDamage);
            StartCoroutine(HitReaction(monster));
        }
    }

    private IEnumerator HitReaction(MonsterStats monster)
    {
        SpriteRenderer renderer = monster != null ? monster.GetComponent<SpriteRenderer>() : null;
        if (renderer == null) yield break;

        for (int i = 0; i < 3; i++)
        {
            if (renderer == null) yield break; // 파괴된 경우 즉시 종료
            renderer.enabled = false;
            yield return new WaitForSeconds(0.1f);

            if (renderer == null) yield break;
            renderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }
}