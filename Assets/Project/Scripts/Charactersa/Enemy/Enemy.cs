using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private EnemyPatrol _patrol;
    [SerializeField] private EnemyChaser _chaser;
    [SerializeField] private FlipHandler _flipHandler;
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.Died += Die;
    }

    private void OnDisable()
    {
        _health.Died -= Die;
    }

    private void Update()
    {
        Vector3 target;

        if (_chaser.HasTarget)
        {
            target = _chaser.TargetPosition;
        }
        else
        {
            target = _patrol.CurrentPoint;
            _patrol.UpdateTarget(transform.position);
        }

        _mover.MoveTo(target);

        float direction = target.x - transform.position.x;

        _flipHandler.Flip(direction);
    }

    private void Die()
    {
        Debug.Log("Enemy is Dead");
        Destroy(gameObject);
    }
}