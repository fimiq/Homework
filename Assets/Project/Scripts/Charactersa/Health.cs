using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;

    public event Action Died;
    public event Action<int> Changed;
    public event Action Damaged;

    public int CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        Debug.Log("Current Health" + CurrentHealth);

        Changed?.Invoke(CurrentHealth);
        Damaged?.Invoke();

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Died?.Invoke();
        }
    }   

    public void Heal(int amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, _maxHealth);

        Changed?.Invoke(CurrentHealth);

        Debug.Log("Heal" + CurrentHealth);
    }
}