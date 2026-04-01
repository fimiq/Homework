using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ColorChanger))]
[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private ColorChanger _colorChanger;
    private Renderer _renderer;
    private Rigidbody _rigidbody;

    private int _lowLimitTimer = 2;
    private int _highLimitTimer = 5;

    private bool _releaseStarted = false;

    private  Coroutine _coroutine;

    public event Action<Cube> Released;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _releaseStarted = false;
        _colorChanger.SetStartColor();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_releaseStarted)
            return;

        if (collision.collider.TryGetComponent(out Platform platform))
        {
            _releaseStarted = true;

            _colorChanger.SetRandomColor();
            _coroutine = StartCoroutine(ReleaseCube());
        }
    }

    public void Release() =>
        Released?.Invoke(this);

    public void ResetState()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.rotation = Quaternion.identity;
    }
     
    private IEnumerator ReleaseCube()
    {
        yield return new WaitForSeconds(GetRandomTime());
        Release();
    }

    private int GetRandomTime() =>
        UnityEngine.Random.Range(_lowLimitTimer, _highLimitTimer + 1);

    private void OnDisable() =>
        StopCoroutine(_coroutine);
}
