using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _delay;

    private Coroutine _coroutine;

    private void Start()
    {
        _coroutine = StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            yield return wait;

            GameObject enemyObject = Instantiate(_enemyPrefab, SelectRandomPoint(), Quaternion.identity);

            if (enemyObject.TryGetComponent(out Enemy enemy))
            {
                enemy.Initialize(GenerateRandomVelocity());
            }
        }   
    }

    private Vector3 SelectRandomPoint()
    {
        return _points[Random.Range(0, _points.Length)].position;
    }

    private Vector3 GenerateRandomVelocity()
    {
        return new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1f,1f)).normalized;
    }
}
