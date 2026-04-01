using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _delay;

    private Transform SelectRandomPoint()
    {
        return _points[Random.Range(0, _points.Length+1)];
    }

    private Vector3 GenerateRandomVelocity()
    {
        return
    }
}
