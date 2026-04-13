using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RogueMover : MonoBehaviour
{
    [SerializeField] private int _speed = 2;

    private Rigidbody _rigidbody;
    private Coroutine _coroutine;

    private Vector3 _direction = Vector3.right;
    private float _delay = 4f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _coroutine = StartCoroutine(Move());
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = _direction * _speed * Time.deltaTime;
    }

    private IEnumerator Move()
    {
        var wait = new WaitForSeconds(_delay);       

        while (enabled)
        {
            yield return wait;
            _speed = -_speed;
        }       
    }
}