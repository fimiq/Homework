using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Target _target;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _delay;
    [SerializeField] private float _lifeTime;

    private Coroutine _coroutine;
    private ObjectPool<Enemy> _pool;
    private int _poolCapacity = 7;
    private int _poolMaxSize = 7;
    private int _activeCount = 0;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(_enemyPrefab),
            actionOnGet: OnGet,
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

    private void OnGet(Enemy enemy)
    {
        enemy.transform.position = _spawnPoint.position;
        enemy.gameObject.SetActive(true);

        enemy.Initialize(_target, _lifeTime);

        enemy.Release += ReleaseEnemy;

    }

    private void OnReleaseToPool(Enemy enemy)
    {
        enemy.Release -= ReleaseEnemy;
        enemy.gameObject.SetActive(false);
    }
}