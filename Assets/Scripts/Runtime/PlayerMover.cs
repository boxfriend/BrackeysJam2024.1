using UnityEngine;
using UnityEngine.Splines;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private SplineController _controller;
    [SerializeField] private SplineAnimate _splineAnimate;

    private void Start () => _splineAnimate.Container = _controller.Container;
    
    public void OnPause(bool isPaused)
    {
        if (isPaused)
            StopMoving();
        else
            Resume();
    }

    public void StopMoving()
    {
        _splineAnimate.Pause();
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _splineAnimate.Play();
    }
}
