using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _delay;

    private Coroutine _coroutine;
    private ObjectPool<Enemy> _pool;
    private int _poolCapacity = 7;
    private int _poolMaxSize = 7;
    private int _activeCount = 0;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_enemyPrefab),
            actionOnGet: GetAction,
            actionOnRelease: OnReleaseToPool,
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start() =>
        _coroutine = StartCoroutine(Spawn());

    private IEnumerator Spawn()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            yield return wait;

            GetEnemy();
        }   
    }

    private void GetEnemy()
    {
        if (_activeCount >= _poolMaxSize)
            return;

        Enemy enemy = _pool.Get();
        enemy.ResetState();
        _activeCount++;
    }

    private void ReleaseEnemy(Enemy enemy)
    {
        _pool.Release(enemy);
        _activeCount--;
    }

    private void GetAction(Enemy enemy)
    {
        enemy.transform.position = SelectRandomPoint();
        enemy.Initialize(GenerateRandomDirection());

        enemy.Release += ReleaseEnemy;

        enemy.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Enemy enemy)
    {
        enemy.Release -= ReleaseEnemy;
        enemy.gameObject.SetActive(false);
    }

    private Vector3 SelectRandomPoint() =>
        _points[Random.Range(0, _points.Length)].position;

    private Vector3 GenerateRandomDirection() =>
        new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1f,1f)).normalized;
}
