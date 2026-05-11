using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Wallet))]
public class Player : MonoBehaviour
{
    private PlayerMover _controller;
    private PlayerAnimator _animator;
    private Wallet _wallet;

    private void Start()
    {
        _controller = GetComponent<PlayerMover>();
        _animator = GetComponent<PlayerAnimator>();
        _wallet = GetComponent<Wallet>();
    }

    private void OnEnable()
    {
        _controller.OnMove += HandleMove;
        _controller.OnCoinCollected += HandleCoinCollected;
    }

    private void OnDisable()
    {
        _controller.OnMove -= HandleMove;
        _controller.OnCoinCollected -= HandleCoinCollected;
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
