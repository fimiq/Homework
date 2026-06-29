using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int _damage = 20;
    [SerializeField] private float _attackCooldown = 0.5f;
    [SerializeField] private float _attackRange = 0.8f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemyLayer;

    private float _nextAttackTime;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= _nextAttackTime)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _nextAttackTime = Time.time + _attackCooldown;

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            _attackPoint.position,
            _attackRange,
            _enemyLayer);

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);
            }
        }
    }
}
