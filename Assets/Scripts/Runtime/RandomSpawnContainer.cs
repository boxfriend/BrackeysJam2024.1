using UnityEngine;

[CreateAssetMenu(fileName = "RandomSpawnContainer.asset", menuName = "Tools/RandomSpawnContainer")]
public class RandomSpawnContainer : ScriptableObject
{
    [SerializeField] private GameObject[] _randomSpawns;

    public GameObject Get () => _randomSpawns[Random.Range(0, _randomSpawns.Length)];
}