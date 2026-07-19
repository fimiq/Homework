using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private EnemyPatrol _patrol;
    [SerializeField] private EnemyChaser _chaser;
    [SerializeField] private FlipHandler _flipHandler;
    [SerializeField] private Health _health;
    [SerializeField] private Attack _attack;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private Transform _player;

    private void Start() =>
        _chaser.SetTarget(_player);

    private void OnEnable() =>
        _health.Died += OnDied;

    private void OnDisable() =>
        _health.Died -= OnDied;

    private void Update()
    {
        Vector3 target;

        if (_chaser.HasTarget)
        {
            target = _chaser.TargetPosition;

            if (_attack.IsTargetInRange() && _attack.TryAttack())
                _animator.SetAttack();
        }
        else
        {
            _patrol.UpdateTarget(transform.position);
            target = _patrol.CurrentPoint;
        }

        float direction = target.x - transform.position.x;
        _flipHandler.Flip(direction);
        _mover.MoveTo(target);
    }

    private void OnDied() =>
        Destroy(gameObject);
}
