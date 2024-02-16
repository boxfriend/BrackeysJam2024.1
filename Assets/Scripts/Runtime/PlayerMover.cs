using UnityEngine;
using UnityEngine.Splines;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private SplineController _controller;
    [SerializeField] private SplineAnimate _splineAnimate;
    [SerializeField] private float _minimumMoveSpeed;

    private void Start ()
    {
        Item.OnMoveSpeedChange += OnMoveSpeedChange;
        _splineAnimate.Container = _controller.Container;
    }

    private void OnDestroy () => Item.OnMoveSpeedChange -= OnMoveSpeedChange;
    private void OnMoveSpeedChange (float speed)
    {
        var oldSpeed = _splineAnimate.MaxSpeed;
        oldSpeed = Mathf.Max(oldSpeed + speed, _minimumMoveSpeed);
        _splineAnimate.MaxSpeed = oldSpeed;
    }

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
