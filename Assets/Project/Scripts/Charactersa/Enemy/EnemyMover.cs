using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _speed = 2f;

    private SpriteRenderer _spriteRenderer;
    private int _currentPointIndex = 0;
    private Transform _targetPoint;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_patrolPoints.Length == 0)
            return;

        _targetPoint = _patrolPoints[_currentPointIndex];
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 current = transform.position;
        Vector3 target = _targetPoint.position;

        float newX = Mathf.MoveTowards(current.x, target.x,_speed * Time.deltaTime);

        transform.position = new Vector3(newX, current.y, current.z);

        if (Mathf.Abs(current.x - target.x) < 0.05f)
        {
            ChangeTarget();
        }
    }

    private void ChangeTarget()
    {
        _currentPointIndex++;

        if (_currentPointIndex >= _patrolPoints.Length)
        {
            _currentPointIndex = 0;
        }

        _targetPoint = _patrolPoints[_currentPointIndex];

        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
}