using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action ClickPerformed;

    private int _inputButton = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(_inputButton))
        {
            ClickPerformed?.Invoke();
        }
    }
}
