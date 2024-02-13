using UnityEngine;

public class ScoreGiver : MonoBehaviour
{
    public void IncreaseScore (int score) => ScoreTracker.Instance.IncreaseScore(score);
}
