using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform Player { get; private set; }
    public Vector2 SpawnPoint { get; private set; }

    [SerializeField] private PatrolState _patrolState;
    [SerializeField] private ChaseState _chaseState;
    [SerializeField] private MonoBehaviour _attackStateBehaviour;

    [SerializeField] private float _visionRadius = 5f;
    [SerializeField] private float _attackRadius = 1.5f;
    [SerializeField] private LayerMask _playerLayer;

    public float AttackRadius => _attackRadius;

    private IState _attackState;
    private MonsterStateContext _fsm;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        SpawnPoint = transform.position;

        _attackState = _attackStateBehaviour as IState;
        if (_attackState == null)
            Debug.LogError("Attack state must implement IState and be assigned to _attackStateBehaviour!");
        Debug.Log($"AttackStateBehaviour: {_attackStateBehaviour}, Type: {_attackStateBehaviour?.GetType()}");

        _fsm = GetComponent<MonsterStateContext>();
    }

    private void Start()
    {
    }

    public void StartPatrol() 
    {
        if (_fsm == null || _patrolState == null)
        {
            return;
        }
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

    public void SetPatrolPoints(Transform[] points)
    {
        if (_patrolState != null)
        {
            _patrolState.SetPatrolPoints(points);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _visionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
}
