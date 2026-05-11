using System;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    public event Action<int> CoinCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            CoinCollected?.Invoke(coin.Value);

            coin.Collect();
        }
    }
}