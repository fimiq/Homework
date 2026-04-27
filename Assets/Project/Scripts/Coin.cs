using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<int> Collected;

    [SerializeField] private int _value = 1;

    public void Collect()
    {
        Collected?.Invoke(_value);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Wallet player))
        {
            Collect();
        }
    }
}
