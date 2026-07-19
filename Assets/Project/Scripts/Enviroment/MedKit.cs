using UnityEngine;

public class MedKit : MonoBehaviour
{
    [SerializeField] private int _healAmount = 30;

    public int HealAmount => _healAmount;

    public void Collect()
    {
        gameObject.SetActive(false);
    }
}
