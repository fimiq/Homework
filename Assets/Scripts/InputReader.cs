using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action OnMouseClick;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClick?.Invoke();
        }
    }
}
