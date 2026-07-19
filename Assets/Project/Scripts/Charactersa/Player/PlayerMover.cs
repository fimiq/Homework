using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(FlipHandler))]
[RequireComponent(typeof(InputReader))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 7f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    public event Action<float> Moved;

    private FlipHandler _flipHandler;
    private Rigidbody2D _rigidbody;
    private InputReader _input;

    private void Awake()
    {
        _flipHandler = GetComponent<FlipHandler>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _input = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _input.JumpPressed += OnJumpPressed;
    }

    private void OnDisable()
    {
        _input.JumpPressed -= OnJumpPressed;
    }

    private void Update()
    {
        float horizontal = _input.Horizontal;

        _rigidbody.linearVelocity = new Vector2(horizontal * _speed, _rigidbody.linearVelocity.y);
        _flipHandler.Flip(horizontal);
        Moved?.Invoke(Mathf.Abs(horizontal));
    }

    private void OnJumpPressed()
    {
        if (IsGrounded())
            Jump();
    }

    private void Jump()
    {
        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, 0f);
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _groundRadius, _groundLayer);
    }
}
