using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerCollector))]
[RequireComponent(typeof(Wallet))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    private PlayerMover _mover;
    private PlayerAnimator _animator;
    private PlayerCollector _collector;
    private Wallet _wallet;
    private Health _health;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _animator = GetComponent<PlayerAnimator>();
        _collector = GetComponent<PlayerCollector>();
        _wallet = GetComponent<Wallet>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _mover.OnMove += HandleMove;
        _collector.CoinCollected += HandleCoinCollected;
        _collector.MedKitCollected += HandleMedKitCollected;
        _health.Died += HandleDied;
    }

    private void OnDisable()
    {
        _mover.OnMove -= HandleMove;
        _collector.CoinCollected -= HandleCoinCollected;
        _collector.MedKitCollected -= HandleMedKitCollected;
        _health.Died -= HandleDied;
    }

    private void HandleMove(float speed)
    {
        _animator.SetMove(speed);
    }

    private void HandleCoinCollected(int value)
    {
        _wallet.Add(value);
    }

    private void HandleMedKitCollected(int healAmount)
    {
        _health.Heal(healAmount);
    }

    private void HandleDied()
    {
        Debug.Log("Player died!");
        gameObject.SetActive(false);
    }
}
