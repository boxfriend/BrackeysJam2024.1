using System;
using UnityEngine;
using Boxfriend.Utils;

public class ScoreTracker : SingletonBehaviour<ScoreTracker>
{
    public int Score { get; private set; }
    public int HighScore { get; private set; }

    public event Action<int, bool> OnScoreUpdated;

    public const string HighScoreKey = "HighScore";

    public void IncreaseScore (int score)
    {
        Score += score;
        OnScoreUpdated?.Invoke(Score, Score > HighScore);
        HighScore = Mathf.Max(Score, HighScore);
    }

    private void Awake () => HighScore = PlayerPrefs.GetInt(HighScoreKey);
    private void OnDestroy () => PlayerPrefs.SetInt(HighScoreKey, HighScore);

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Tools/Boxfriend/Reset High Score")]
#endif
    public static void ResetHighScore () => PlayerPrefs.DeleteKey(HighScoreKey);
}
