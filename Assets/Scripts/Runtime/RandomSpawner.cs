using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _randomSpawns;
    [SerializeField] private Transform _spawnPoint;

    public void Spawn()
    {
        var rng = Random.Range(0, _randomSpawns.Length);
        Instantiate(_randomSpawns[rng], _spawnPoint.position, _spawnPoint.rotation);
    }
}
