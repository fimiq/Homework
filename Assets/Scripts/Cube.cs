using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    public event Action<Cube> OnReleased;

    private int _lowLimitTimer = 2;
    private int _highLimitTimer = 5;

    public void Release()
    {
        OnReleased?.Invoke(this);
    }

    public void ResetState()
    {
        if (TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.linearVelocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.rotation = Quaternion.identity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Platform")
        {
            Invoke(nameof(Release), GetRandomTime());
        }
    }

    private int GetRandomTime() =>
        UnityEngine.Random.Range(_lowLimitTimer, _highLimitTimer + 1);

    private void OnDisable() =>
        CancelInvoke();

}
