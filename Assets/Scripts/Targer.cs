using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Target : MonoBehaviour
{
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] private float _speed = 1f;

    private float _distance = 0.1f;
    private Rigidbody _rigidbody;
    private Vector3 _target;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _target = _pointB.position;
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = Vector3.MoveTowards(_rigidbody.position, _target, _speed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(newPosition);

        if (Vector3.Distance(_rigidbody.position, _target) < _distance)
        {
            if (_target == _pointA.position)
            {
                _target = _pointB.position;
            }
            else
            {
                _target = _pointA.position;
            }
        }
    }
}
