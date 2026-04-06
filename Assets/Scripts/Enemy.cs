using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour
{
    public event Action<Enemy> Release;

    protected Rigidbody _rigidbody;
    protected Coroutine _coroutine;
    protected Target _target;
    protected float _lifeTime;

    public void Awake() =>
        _rigidbody = GetComponent<Rigidbody>();

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    public virtual void Initialize(Target target, float lifeTime)
    {
        _target = target;
        _lifeTime = lifeTime;
        _coroutine = StartCoroutine(DisableByTime());
    }

    public void ResetState()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero; 
        _rigidbody.rotation = Quaternion.identity;
    }

    protected IEnumerator DisableByTime()
    {
        yield return new WaitForSeconds(_lifeTime);

        Release?.Invoke(this);
    }
}
