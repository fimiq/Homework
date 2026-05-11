using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<Coin> Collected;

    public int Value { get; private set; } = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Wallet player))
        {
            Collect();
        }
    }

    public void Collect()
    {
        Collected?.Invoke(this);
        gameObject.SetActive(false);
    }   
}
