using UnityEngine;
using UnityEngine.InputSystem;

public class GameEndSequence : MonoBehaviour
{
    [SerializeField] private string _tag;

    [SerializeField] private TimerDisplay _toActivate;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private PlayerPauser _pauser;

    private void OnTriggerEnter (Collider other)
    {
        if(other.CompareTag(_tag))
        {
            _toActivate.OnTimerEnd += OnEnd;
            _toActivate.Activate();
        }
    }

    private void OnEnd()
    {
        _pauser.Pause(false);
        _gameOverScreen.SetActive(true);
        _toActivate.gameObject.SetActive(false);
    }
}
