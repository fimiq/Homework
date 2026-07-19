using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Attack))]
public class Player : MonoBehaviour
{
    private PlayerMover _mover;
    private PlayerAnimator _animator;
    private Health _health;
    private InputReader _input;
    private Attack _attack;

    private void Awake()
    {
        _mover = GetComponent<PlayerMover>();
        _animator = GetComponent<PlayerAnimator>();
        _health = GetComponent<Health>();
        _input = GetComponent<InputReader>();
        _attack = GetComponent<Attack>();
    }

    private void OnEnable()
    {
        _mover.Moved += OnMoved;
        _health.Died += OnDied;
        _input.AttackPressed += OnAttackPressed;
    }

    private void OnDisable()
    {
        _mover.Moved -= OnMoved;
        _health.Died -= OnDied;
        _input.AttackPressed -= OnAttackPressed;
    }

    private void OnMoved(float speed) => 
        _animator.SetMove(speed);

    private void OnAttackPressed()
    {
        if (_attack.TryAttack())
            _animator.SetAttack();
    }

    private void OnDied()
    {
        Debug.Log("Player died!");
        gameObject.SetActive(false);
    }
}
