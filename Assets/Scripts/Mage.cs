using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Mage : Enemy
{
    [SerializeField] private float _speed = 2f;

    public override void Initialize(Target target, float lifeTime)
    {
        _target = target;
        _lifeTime = lifeTime;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(DisableByTime());
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            Vector3 direction = (_target.gameObject.transform.position - transform.position).normalized;
            _rigidbody.linearVelocity = direction * _speed;
        }
    }
}
