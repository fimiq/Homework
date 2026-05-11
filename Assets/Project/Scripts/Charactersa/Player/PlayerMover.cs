using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(FlipHandler))]
public class PlayerMover : MonoBehaviour
{
    public event Action<float> OnMove;
    public event Action<int> OnCoinCollected;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 7f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    private const string HorizontalAxis = "Horizontal";

    private bool _isFaceRight = true;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
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

        if (horizontal > 0 && !_isFaceRight)
            FlipCharacter();
        else if (horizontal < 0 && _isFaceRight)
            FlipCharacter();
    }

    private void FlipCharacter()
    {
        _isFaceRight = !_isFaceRight;

        transform.rotation = Quaternion.Euler(0, 180, 0);
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