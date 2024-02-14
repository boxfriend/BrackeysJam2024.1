using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRandomizer : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private int _maxRandom;
    [SerializeField] private string _parameterName;
    [SerializeField] private bool _startsFromZero;

    private void Awake ()
    {
        var index = Random.Range(0, _maxRandom);

        if (!_startsFromZero)
            index++;

        _animator.SetInteger(_parameterName, index);
    }
}
