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
    private Color[] _originalColors;

    private void Awake()
    {
        _health = GetComponent<Health>();

        if (_renderers == null || _renderers.Length == 0)
            _renderers = GetComponentsInChildren<SpriteRenderer>();

        _originalColors = new Color[_renderers.Length];

        for (int i = 0; i < _renderers.Length; i++)
            _originalColors[i] = _renderers[i].color;
    }

    private void OnEnable()  => _health.Damaged += OnDamaged;
    private void OnDisable() => _health.Damaged -= OnDamaged;

    private void OnDamaged()
    {
        if (_flashCoroutine != null)
            StopCoroutine(_flashCoroutine);

        _flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        for (int flash = 0; flash < _flashCount; flash++)
        {
            SetColor(_flashColor);
            yield return new WaitForSeconds(_flashDuration);

            RestoreColors();
            yield return new WaitForSeconds(_flashDuration);
        }

        _flashCoroutine = null;
    }

    private void SetColor(Color color)
    {
        foreach (SpriteRenderer sr in _renderers)
            sr.color = color;
    }

    private void RestoreColors()
    {
        for (int i = 0; i < _renderers.Length; i++)
            _renderers[i].color = _originalColors[i];
    }
}
