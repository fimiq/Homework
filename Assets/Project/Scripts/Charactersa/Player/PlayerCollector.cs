using System;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    public event Action<int> CoinCollected;
    public event Action<int> MedKitCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            CoinCollected?.Invoke(coin.Value);
            coin.Collect();
        }

        if (collision.TryGetComponent(out MedKit medKit))
        {
            MedKitCollected?.Invoke(medKit.HealAmount);
            medKit.Collect();
        }
    }
}
