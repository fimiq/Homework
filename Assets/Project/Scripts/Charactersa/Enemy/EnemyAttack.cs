using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _attackCooldown = 1f;

    private float _nextAttackTime;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryAttack(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        TryAttack(collision.gameObject);
    }

    private void TryAttack(GameObject target)
    {
        if (Time.time < _nextAttackTime)
            return;

        if (target.TryGetComponent(out Health health))
        {
            _nextAttackTime = Time.time + _attackCooldown;
            health.TakeDamage(_damage);
        }
    }
}
