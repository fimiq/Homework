using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DamageFlash : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _renderers;
    [SerializeField] private Color _flashColor = Color.red;
    [SerializeField] private float _flashDuration = 0.1f;
    [SerializeField] private int _flashCount = 3;

    private Health _health;
    private Coroutine _flashCoroutine;

    private void Awake()
    {
        _health = GetComponent<Health>();

        if (_renderers == null || _renderers.Length == 0)
            _renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _health.Damaged += OnDamaged;
    }

    private void OnDisable()
    {
        _health.Damaged -= OnDamaged;
    }

    private void OnDamaged()
    {
        if (_flashCoroutine != null)
            StopCoroutine(_flashCoroutine);

        _flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        Color[] originalColors = new Color[_renderers.Length];

        for (int i = 0; i < _renderers.Length; i++)
            originalColors[i] = _renderers[i].color;

        for (int flash = 0; flash < _flashCount; flash++)
        {
            SetColor(_flashColor);
            yield return new WaitForSeconds(_flashDuration);

            RestoreColors(originalColors);
            yield return new WaitForSeconds(_flashDuration);
        }

        _flashCoroutine = null;
    }

    private void SetColor(Color color)
    {
        foreach (SpriteRenderer renderer in _renderers)
            renderer.color = color;
    }

    private void RestoreColors(Color[] colors)
    {
        for (int i = 0; i < _renderers.Length; i++)
            _renderers[i].color = colors[i];
    }
}