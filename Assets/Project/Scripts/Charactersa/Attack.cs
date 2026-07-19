using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private int _damage = 20;
    [SerializeField] private float _attackCooldown = 0.5f;
    [SerializeField] private float _attackRange = 0.8f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _targetLayer;

    private float _nextAttackTime;

    public bool IsTargetInRange()
    {
        return Physics2D.OverlapCircle(
            _attackPoint.position,
            _attackRange,
            _targetLayer);
    }

    public bool TryAttack()
    {
        if (Time.time < _nextAttackTime)
            return false;

        _nextAttackTime = Time.time + _attackCooldown;

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            _attackPoint.position,
            _attackRange,
            _targetLayer);

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out Health health))
                health.TakeDamage(_damage);
        }

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
