using Boxfriend.Utils;
using UnityEngine;
using UnityEngine.Audio;

public class Destroyable : MonoBehaviour
{
    [SerializeField] private ParticleSystem _deathParticles;
    [SerializeField] private Transform _particleSpawnPoint;
    public void OnDie()
    {
        var particles = Instantiate(_deathParticles, _particleSpawnPoint.position, _particleSpawnPoint.rotation);
        Destroy(particles.gameObject, 3f);
        Destroy(gameObject);
    }
}
