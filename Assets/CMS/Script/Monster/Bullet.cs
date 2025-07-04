using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _lifeTime = 3f;

    private Vector2 _direction;
    private bool _initialized = false;
    private bool _hasHit = false;

    public void SetDirection(Vector2 dir)
    {
        _direction = dir.normalized;
        _initialized = true;
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        if (!_initialized || _hasHit) return;

        transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_hasHit) return;

        if (collision.CompareTag("Player"))
        {
            PlayerStats player = collision.GetComponent<PlayerStats>();
            if (player != null)
            {
                _hasHit = true;
                player.TakeDamage(_damage);
                Destroy(gameObject);
            }
        } 
    }
}

