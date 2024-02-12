using Boxfriend.Utils;
using UnityEngine;
using UnityEngine.Audio;

public class Door : MonoBehaviour
{
    [SerializeField] private ParticleSystem _deathParticles;
    [SerializeField] private Transform _particleSpawnPoint;

    [SerializeField] private AudioResource _hitSound;
    [SerializeField] private AudioResource _breakSound;

    public void OnDie()
    {
        //AudioManager.Instance.Play(_breakSound, transform.position);
        var particles = Instantiate(_deathParticles, _particleSpawnPoint.position, _particleSpawnPoint.rotation);
        Destroy(particles.gameObject, 3f);
        Destroy(gameObject);
    }

    public void OnDamage()
    {
        AudioManager.Instance.Play(_hitSound, transform.position);
        //Do something visual here
    }
}