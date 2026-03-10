using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void OnEnable()
    {
        _score.OnScoreChanged += DisplayScore;
    }

    private void OnDisable()
    {
        _score.OnScoreChanged -= DisplayScore;
    }

    private void DisplayScore(int score)
    {
        _scoreText.text = score.ToString("");
    }
}
