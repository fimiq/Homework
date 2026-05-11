using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    public event Action<Coin> CoinSpawned;

    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private float _delay = 3f;

    [SerializeField] private int _maxActiveCoins = 3;

    private ObjectPool<Coin> _coinPool;
    private int _activeCoins = 0;

    private void Awake()
    {
        _coinPool = new ObjectPool<Coin>(
            createFunc: CreateCoin,
            actionOnGet: OnGetCoin,
            actionOnRelease: OnReleaseCoin,
            actionOnDestroy: coin => Destroy(coin.gameObject),
            collectionCheck: true,
            defaultCapacity: _maxActiveCoins,
            maxSize: _maxActiveCoins
        );
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        var wait = new WaitForSeconds(_delay);

        while (true)
        {
            yield return wait;
            SpawnCoin();
        }
    }

    private void SpawnCoin()
    {
        if (_activeCoins >= _maxActiveCoins)
            return;

        _coinPool.Get();
    }

    private Coin CreateCoin()
    {
        Coin coin = Instantiate(_coinPrefab);
        coin.gameObject.SetActive(false);
        return coin;
    }

    private void OnGetCoin(Coin coin)
    {
        Transform spawnPoint = GetRandomSpawnPoint();

        coin.transform.position = spawnPoint.position;
        coin.gameObject.SetActive(true);

        coin.Collected += OnCoinCollected;

        _activeCoins++;
    }

    private void OnReleaseCoin(Coin coin)
    {
        _activeCoins--;

        coin.Collected -= OnCoinCollected;
        coin.gameObject.SetActive(false);
    }

    private void OnCoinCollected(Coin coin)
    {
        ReleaseCoin(coin);
    }

    public void ReleaseCoin(Coin coin)
    {
        _coinPool.Release(coin);
    }

    private Transform GetRandomSpawnPoint()
    {
        return _spawnPositions[UnityEngine.Random.Range(0, _spawnPositions.Length)];
    }
}