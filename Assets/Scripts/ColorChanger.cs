using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    private Renderer _renderer;
    private bool _hasChanged = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _hasChanged = false;
        SetStartColor();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if((collision.collider.tag == "Platform"))
        {
            if (_hasChanged)
            {
                return;
            }

            SetRandomColor();
            _hasChanged = true;
        }       
    }

    private void SetRandomColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }

    private void SetStartColor()
    {
        _renderer.material.color = Color.white;
    }
}