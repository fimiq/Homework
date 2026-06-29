using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class MedKitSpawner : MonoBehaviour
{
    [SerializeField] private MedKit _medKitPrefab;
    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private float _delay = 8f;
    [SerializeField] private int _maxActiveMedKits = 2;

    private ObjectPool<MedKit> _pool;
    private int _activeMedKits = 0;

    private void Awake()
    {
        _pool = new ObjectPool<MedKit>(
            createFunc: CreateMedKit,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: kit => Destroy(kit.gameObject),
            collectionCheck: true,
            defaultCapacity: _maxActiveMedKits,
            maxSize: _maxActiveMedKits
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

            if (_activeMedKits < _maxActiveMedKits)
                _pool.Get();
        }
    }

    private MedKit CreateMedKit()
    {
        MedKit kit = Instantiate(_medKitPrefab);
        kit.gameObject.SetActive(false);
        return kit;
    }

    private void OnGet(MedKit kit)
    {
        Transform spawnPoint = _spawnPositions[Random.Range(0, _spawnPositions.Length)];
        kit.transform.position = spawnPoint.position;
        kit.gameObject.SetActive(true);
        kit.Collected += OnMedKitCollected;
        _activeMedKits++;
    }

    private void OnRelease(MedKit kit)
    {
        _activeMedKits--;
        kit.Collected -= OnMedKitCollected;
        kit.gameObject.SetActive(false);
    }

    private void OnMedKitCollected(MedKit kit)
    {
        _pool.Release(kit);
    }
}
