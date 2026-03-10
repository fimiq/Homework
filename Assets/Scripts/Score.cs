using UnityEngine;
using System.Collections;
using System;

public class Score : MonoBehaviour
{
    public event Action<int> OnScoreChanged;

    [SerializeField] private InputReader _inputReader;

    private int _score;
    private bool _isRunning;
    private IEnumerator _coroutine;

    private void Start()
    {
        _coroutine = ScoreIncrease(0.5f, 1);
    }

    private void OnEnable()
    {
        _inputReader.ClickPerformed += ToggleCounter;
    }

    private void OnDisable()
    {
        _inputReader.ClickPerformed -= ToggleCounter;
    }

    private IEnumerator ScoreIncrease(float delay, int amount)
    {
        var wait = new WaitForSeconds(delay);

        while (enabled)
        {
            yield return wait;

            _score += amount;
            OnScoreChanged?.Invoke(_score);
        }
    }

    public void ToggleCounter()
    {
        if (_isRunning)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
        else
        {
            StartCoroutine(_coroutine);
        }

        _isRunning = !_isRunning;
    }
}
