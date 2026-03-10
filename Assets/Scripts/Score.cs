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
        _inputReader.OnMouseClick += ToggleCounter;
    }

    private void OnDisable()
    {
        _inputReader.OnMouseClick -= ToggleCounter;
    }

    private IEnumerator ScoreIncrease(float delay, int amount)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);

            _score += amount;
            OnScoreChanged?.Invoke(_score);
        }     
    }

    public void ToggleCounter()
    {
        if (_isRunning)
        {
            StopCoroutine(_coroutine);
            _isRunning = false;
        }
        else
        {
            StartCoroutine(_coroutine);
            _isRunning = true;
        }
    }
}
