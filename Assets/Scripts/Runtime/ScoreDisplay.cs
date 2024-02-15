using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreDisplay, _highScoreDisplay;
    void Start()
    {
        var scoreTracker = ScoreTracker.Instance;

        _highScoreDisplay.text = scoreTracker.HighScore.ToString("N0");
        _scoreDisplay.text = scoreTracker.Score.ToString("N0");

        scoreTracker.OnScoreUpdated += UpdateScore;
    }

    private void UpdateScore(int newScore, bool newHighScore)
    {
        _scoreDisplay.text = newScore.ToString();

        if (newHighScore)
            _highScoreDisplay.text = newScore.ToString();
    }
}
