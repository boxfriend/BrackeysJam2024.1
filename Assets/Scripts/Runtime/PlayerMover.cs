using UnityEngine;
using UnityEngine.Splines;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private SplineController _controller;
    [SerializeField] private SplineAnimate _splineAnimate;

    private void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _splineAnimate.Container = _controller.Container;
        _splineAnimate.Play();
    }

}
