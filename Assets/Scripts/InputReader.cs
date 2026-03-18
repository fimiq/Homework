using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private int _inputButton = 0;

    public event Action ClickPerformed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(_inputButton))
        {
            ClickPerformed?.Invoke();
        }
    }
}
