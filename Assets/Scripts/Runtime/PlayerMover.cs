using UnityEngine;
using UnityEngine.Splines;
using Vertx.Attributes;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private SplineController _controller;
    [SerializeField] private SplineAnimate _splineAnimate;

    private void Start ()
    {
        _splineAnimate.Container = _controller.Container;
        _splineAnimate.Play();
    }

}
