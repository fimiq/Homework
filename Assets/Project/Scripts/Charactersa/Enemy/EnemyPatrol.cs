using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] _points;

    private const float ReachDistance = 0.05f;

    private int _currentIndex;

    public Vector3 CurrentPoint => _points[_currentIndex].position;

    public void UpdateTarget(Vector3 currentPosition)
    {
        if (Mathf.Abs(currentPosition.x - CurrentPoint.x) < ReachDistance)
        {
            _currentIndex = (_currentIndex + 1) % _points.Length;
        }
    }
}