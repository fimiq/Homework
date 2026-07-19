using UnityEngine;


public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _reachDistance = 0.2f;

    private int _currentIndex;

    public Vector3 CurrentPoint => _points[_currentIndex].position;

    public void UpdateTarget(Vector3 currentPosition)
    {
        Vector2 diff = currentPosition - CurrentPoint;

        if (diff.sqrMagnitude < _reachDistance * _reachDistance)
            _currentIndex = (_currentIndex + 1) % _points.Length;
    }
}
