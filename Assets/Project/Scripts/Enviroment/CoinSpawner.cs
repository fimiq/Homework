using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private float _delay = 3f;
    [SerializeField] private int _maxActiveCoins = 3;

    private ObjectPool<Coin> _pool;
    private int _activeCoins;

    private void Awake()
    {
        _pool = new ObjectPool<Coin>(
            createFunc: CreateCoin,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: coin => Destroy(coin.gameObject),
            collectionCheck: true,
            defaultCapacity: _maxActiveCoins,
            maxSize: _maxActiveCoins
        );
    }

    private void Start() => StartCoroutine(SpawnRoutine());

    public void Release(Coin coin) => _pool.Release(coin);

    private IEnumerator SpawnRoutine()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            yield return wait;

            if (_activeCoins < _maxActiveCoins)
                _pool.Get();
        }
    }

    private Coin CreateCoin()
    {
        Coin coin = Instantiate(_coinPrefab);
        coin.gameObject.SetActive(false);
        return coin;
    }

    private void OnGet(Coin coin)
    {
        Transform spawnPoint = _spawnPositions[Random.Range(0, _spawnPositions.Length)];
        coin.transform.position = spawnPoint.position;
        coin.gameObject.SetActive(true);
        _activeCoins++;
    }

    private void OnRelease(Coin coin)
    {
        _activeCoins--;
        coin.gameObject.SetActive(false);
    }
}
