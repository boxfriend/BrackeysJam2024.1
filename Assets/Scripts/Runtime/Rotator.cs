using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _degreesPerSecond;
    [SerializeField] private Vector3 _axis;
    [SerializeField] private bool _unscaledDeltaTime;
    void Update()
    {
        var delta = _unscaledDeltaTime ? Time.unscaledDeltaTime : Time.deltaTime;
        transform.Rotate(_axis * (_degreesPerSecond * delta));
    }

    private void OnValidate () => _axis = _axis.normalized;
}
