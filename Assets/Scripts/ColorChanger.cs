using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        if (TryGetComponent(out Renderer renderer))
        {
            _renderer = renderer;
        }
    }

    public void SetRandomColor() =>
        _renderer.material.color = Random.ColorHSV();

    public void SetStartColor() =>
        _renderer.material.color = Color.white;
}