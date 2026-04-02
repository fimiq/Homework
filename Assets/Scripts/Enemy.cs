using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 1;

    private Vector3 _velocity;
    private Rigidbody _rigidbody;

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = _velocity * _speed;
    }

    public void Initialize(Vector3 velocity)
    {
        _velocity = velocity;
    }   
}
