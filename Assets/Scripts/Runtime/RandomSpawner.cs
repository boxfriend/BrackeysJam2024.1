using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private RandomSpawnContainer _randomSpawns;
    [SerializeField] private Transform _spawnPoint;

    public void Spawn () => Instantiate(_randomSpawns.Get(), _spawnPoint.position, _spawnPoint.rotation);
}