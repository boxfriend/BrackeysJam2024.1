using System;
using UnityEngine;
using Boxfriend.Utils;

public class ScoreTracker : SingletonBehaviour<ScoreTracker>
{
    public int Score { get; private set; }
    public int HighScore { get; private set; }

    public event Action<int, bool> OnScoreUpdated;

    private const string _highScoreKey = "HighScore";

    public void IncreaseScore (int score)
    {
        Score += score;
        OnScoreUpdated?.Invoke(Score, Score > HighScore);
        HighScore = Mathf.Max(Score, HighScore);
    }

    private void Awake () => HighScore = PlayerPrefs.GetInt(_highScoreKey);
    private void OnDestroy () => PlayerPrefs.SetInt(_highScoreKey, HighScore);

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Tools/Boxfriend/Reset High Score")]
    private static void ResetHighScore () => PlayerPrefs.DeleteKey(_highScoreKey);
#endif
}