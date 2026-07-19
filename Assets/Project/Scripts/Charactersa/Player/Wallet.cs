using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public event Action<int> Changed;

    public int Coins { get; private set; }

    public void Add(int amount)
    {
        if (amount <= 0)
            return;

        Coins += amount;
        Changed?.Invoke(Coins);
    }
}
