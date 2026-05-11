using UnityEngine;

public class FlipHandler : MonoBehaviour
{
    private const float LeftRotationY = 180f;
    private const float RightRotationY = 0f;

    private bool _isFacingRight = true;

    public void Flip(float direction)
    {
        if (direction > 0 && _isFacingRight == false)
            RotateRight();
        else if (direction < 0 && _isFacingRight)
            RotateLeft();
    }

    private void RotateRight()
    {
        _isFacingRight = true;
        transform.rotation = Quaternion.Euler(0f, RightRotationY, 0f);
    }

    private void RotateLeft()
    {
        _isFacingRight = false;
        transform.rotation = Quaternion.Euler(0f, LeftRotationY, 0f);
    }
}