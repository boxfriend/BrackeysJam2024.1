using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerPauser : MonoBehaviour
{
    [SerializeField] private InputAction _pauseAction;
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private UnityEvent<bool> _onPause;

    private void Awake () => _pauseAction.performed += Pause;
    private void OnEnable () => _pauseAction.Enable();
    private void OnDisable () => _pauseAction.Disable();
    private void OnDestroy () => _pauseAction.Dispose();

    private void Pause (InputAction.CallbackContext ctx) => Pause(true);
    public void Pause(bool withPauseScreen)
    {
        if (withPauseScreen)
            _pauseScreen.SetActive(true);

        _onPause?.Invoke(true);
        enabled = false;
    }

    public void Resume()
    {
        _pauseScreen.SetActive(false);
        _onPause?.Invoke(false);
        enabled = true;
    }
}