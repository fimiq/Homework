using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _lifeTime = 5;

    public event Action<Enemy> Release;

    private Coroutine _coroutine;
    private Vector3 _direction;
    private Rigidbody _rigidbody;

    public void Awake() =>
        _rigidbody = GetComponent<Rigidbody>();

    private void OnEnable() =>
        _coroutine = StartCoroutine(DisableByTime());

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = _direction * _speed;
    }

    public void Initialize(Vector3 direction) =>
        _direction = direction;

    public void ResetState()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.rotation = Quaternion.identity;
    }

    private IEnumerator DisableByTime()
    {
        yield return new WaitForSeconds(_lifeTime);

        Release?.Invoke(this);
    }
}
