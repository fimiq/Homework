using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _spawnPositionXLimit;
    [SerializeField] private float _delay = 1f;

    private int _poolCapacity = 5;
    private int _poolMaxSize = 5;
    private int _activeCount = 0;

    private ObjectPool<Cube> _cubePool;

    private void Awake()
    {
        _cubePool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: GetAction,
            actionOnRelease: OnReleaseToPool,
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start() =>
        StartCoroutine(SpawnObjects());

    private IEnumerator SpawnObjects()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            yield return wait;
            GetCube();
        }
    }

    private void GetCube()
    {
        if (_activeCount >= _poolMaxSize)
            return;

        Cube cube = _cubePool.Get();
        cube.ResetState();
        _activeCount++;
    }

    private void ReleaseCube(Cube cube)
    {
        _cubePool.Release(cube);
        _activeCount--;
    }

    private void GetAction(Cube cube)
    {
        cube.transform.position = GenerateRandomPosition();
        cube.gameObject.SetActive(true);

        cube.Released += ReleaseCube;
    }

    private void OnReleaseToPool(Cube cube)
    {
        cube.Released -= ReleaseCube;
        cube.gameObject.SetActive(false);
    }

    private Vector3 GenerateRandomPosition()
    {
        float randomPositionX = Random.Range(_spawnPosition.position.x - _spawnPositionXLimit, _spawnPosition.position.x + _spawnPositionXLimit);

        return new Vector3(randomPositionX, _spawnPosition.position.y, _spawnPosition.position.z);
    }
}
