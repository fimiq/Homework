using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(FlipHandler))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 7f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    private const string HorizontalAxis = "Horizontal";

    private FlipHandler _flipHandler;
    private Rigidbody2D _rigidbody;

    public event Action<float> OnMove;
    public event Action<int> OnCoinCollected;

    private void Start()
    {
        _flipHandler = GetComponent<FlipHandler>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();

        if (Input.GetButtonDown("Jump") && OnGround())
        {
            Jump();
        }   
    }

    private void Move()
    {
        float horizontal = Input.GetAxis(HorizontalAxis);

        _rigidbody.linearVelocity = new Vector2(horizontal * _speed, _rigidbody.linearVelocity.y);

        _flipHandler.Flip(horizontal);
            
        OnMove?.Invoke(Mathf.Abs(horizontal));
    }

    private void Jump()
    {
        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, 0f);
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private bool OnGround()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _groundRadius, _groundLayer);
    }
}