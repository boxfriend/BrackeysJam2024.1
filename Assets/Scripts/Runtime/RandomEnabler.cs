using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnabler : MonoBehaviour
{
    [SerializeField] private GameObject[] _enableChoices;
    private void Awake ()
    {
        var rng = Random.Range(0, _enableChoices.Length);
        for(var i = 0; i < _enableChoices.Length; i++)
        {
            _enableChoices[i].SetActive(i == rng);
        }
    }
}
