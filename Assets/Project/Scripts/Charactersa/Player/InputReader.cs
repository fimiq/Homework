using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string JumpButton = "Jump";
    private const string AttackButton = "Fire1";

    public event Action JumpPressed;
    public event Action AttackPressed;

    public float Horizontal => Input.GetAxis(HorizontalAxis);

    private void Update()
    {
        if (Input.GetButtonDown(JumpButton))
            JumpPressed?.Invoke();

        if (Input.GetButtonDown(AttackButton))
            AttackPressed?.Invoke();
    }
}
