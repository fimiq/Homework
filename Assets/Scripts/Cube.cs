using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ColorChanger))]
public class Cube : MonoBehaviour
{
    private ColorChanger _colorChanger;
    private Renderer _renderer;

    private int _lowLimitTimer = 2;
    private int _highLimitTimer = 5;

    private bool _releaseStarted = false;

    private  Coroutine _coroutine;

    public event Action<Cube> CubeReleasing;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _releaseStarted = false;
        _colorChanger.SetStartColor(_renderer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_releaseStarted)
            return;

        if (collision.collider.TryGetComponent(out Platform platform))
        {
            _releaseStarted = true;

            _colorChanger.SetRandomColor(_renderer);
            _coroutine = StartCoroutine(ReleaseCube());
        }
    }

    public void Release() =>
        CubeReleasing?.Invoke(this);

    public void ResetState()
    {
        if (TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.linearVelocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.rotation = Quaternion.identity;
        }
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
