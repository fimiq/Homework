using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private EnemyPatrol _patrol;
    [SerializeField] private FlipHandler _flipHandler;

    private void Update()
    {
        Vector3 target = _patrol.CurrentPoint;

        _mover.MoveTo(target);

        float direction = target.x - transform.position.x;

        _flipHandler.Flip(direction);

        _patrol.UpdateTarget(transform.position);
    }
}