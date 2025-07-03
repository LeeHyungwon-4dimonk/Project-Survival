using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform Player { get; private set; }
    public Vector2 SpawnPoint { get; private set; }

    [SerializeField] private PatrolState _patrolState;
    [SerializeField] private ChaseState _chaseState;
    [SerializeField] private AttackState _attackState;

    [SerializeField] private float _visionRadius = 5f;
    [SerializeField] private float _attackRadius = 1.5f;
    [SerializeField] private LayerMask _playerLayer;

    public float AttackRadius => _attackRadius;

    private MonsterStateContext _fsm;
    private IState _lastState;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        SpawnPoint = transform.position;
        _fsm = new MonsterStateContext(this);
    }

    private void Start()
    {
        _fsm.Transition(_patrolState);
    }

    private void Update()
    {
        bool playerInVision = Player != null && Vector2.Distance(transform.position, Player.position) <= _visionRadius;
        bool playerInAttack = Player != null && Vector2.Distance(transform.position, Player.position) <= _attackRadius;

        IState nextState = _patrolState;
        if (playerInAttack) nextState = _attackState;
        else if (playerInVision) nextState = _chaseState;

        if (_fsm.CurrentState != nextState)
        {
            _fsm.Transition(nextState);
        }

        _fsm.CurrentState?.UpdateState();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _visionRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
}
