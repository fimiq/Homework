using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Wallet))]
public class PlayerCollector : MonoBehaviour
{
    [SerializeField] private CoinSpawner _coinSpawner;
    [SerializeField] private MedKitSpawner _medKitSpawner;

    private Health _health;
    private Wallet _wallet;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _wallet = GetComponent<Wallet>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            _wallet.Add(coin.Value);
            coin.Collect();
            _coinSpawner.Release(coin);
            return;
        }

        if (other.TryGetComponent(out MedKit medKit))
        {
            _health.Heal(medKit.HealAmount);
            medKit.Collect();
            _medKitSpawner.Release(medKit);
        }
    }
}
