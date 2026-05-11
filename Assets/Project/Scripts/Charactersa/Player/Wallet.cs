using UnityEngine;
using System;

public class Wallet : MonoBehaviour
{
    public int Coins { get; private set; }

    public event Action<int> OnChanged;

    public void Add(int amount)
    {
        Coins += amount;
        OnChanged?.Invoke(Coins);
    }
}