using System;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    [SerializeField] private int _healAmount = 30;

    public event Action<MedKit> Collected;

    public int HealAmount => _healAmount;

    public void Collect()
    {
        Collected?.Invoke(this);
        gameObject.SetActive(false);
    }
}
