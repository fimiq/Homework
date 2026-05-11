using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerCollector))]
[RequireComponent(typeof(Wallet))]
public class Player : MonoBehaviour
{
    private PlayerMover _mover;
    private PlayerAnimator _animator;
    private PlayerCollector _collector;
    private Wallet _wallet;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _animator = GetComponent<PlayerAnimator>();
        _collector = GetComponent<PlayerCollector>();
        _wallet = GetComponent<Wallet>();
    }

    private void OnEnable()
    {
        _mover.OnMove += HandleMove;
        _collector.CoinCollected += HandleCoinCollected;
    }

    private void OnDisable()
    {
        _mover.OnMove -= HandleMove;
        _collector.CoinCollected -= HandleCoinCollected;
    }

    private void HandleMove(float speed)
    {
        _animator.SetMove(speed);
    }

    private void HandleCoinCollected(int value)
    {
        _wallet.Add(value);
    }
}