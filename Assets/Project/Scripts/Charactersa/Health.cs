using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;

    public event Action Died;
    public event Action Damaged;
    public event Action<int> Changed;

    public int Current  { get; private set; }
    public int Max      => _maxHealth;

    private void Awake()
    {
        Current = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        Current = Mathf.Clamp(Current - damage, 0, _maxHealth);

        Damaged?.Invoke();
        Changed?.Invoke(Current);

        if (Current == 0)
            Died?.Invoke();
    }

    public void Heal(int amount)
    {
        if (amount <= 0)
            return;

        Current = Mathf.Clamp(Current + amount, 0, _maxHealth);

        Changed?.Invoke(Current);
    }
}
