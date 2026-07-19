using UnityEngine;

public class FlipHandler : MonoBehaviour
{
    private readonly Quaternion _rotationRight = Quaternion.Euler(0f, 0f, 0f);
    private readonly Quaternion _rotationLeft  = Quaternion.Euler(0f, 180f, 0f);

    private bool _isFacingRight = true;

    public bool IsFacingRight => _isFacingRight;

    public void Flip(float direction)
    {
        if (direction > 0f && !_isFacingRight)
            RotateTo(true);
        else if (direction < 0f && _isFacingRight)
            RotateTo(false);
    }

    private void RotateTo(bool facingRight)
    {
        _isFacingRight = facingRight;
        transform.rotation = facingRight ? _rotationRight : _rotationLeft;
    }
}
