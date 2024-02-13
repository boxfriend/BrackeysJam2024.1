using Boxfriend.Utils;
using Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [Header("Meta")]
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private InputAction _inputAction;
    [Space(5)]
    [SerializeField] private float _delayTime;
    private float _lastFireTime;
    private ObjectPool<Rigidbody> _bulletPool;

    [Header("VFX")]
    [SerializeField] private CinemachineImpulseSource _impulse;
    [SerializeField] private float _recoilForce;
    [SerializeField] private Rigidbody _prefab;
    [SerializeField] private float _bulletForce;

    [Header("SFX")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioResource _clip;

    private void Awake ()
    {
        _inputAction.performed += (_) => OnShoot();
        _bulletPool = new(CreateBullet, GetFromPool, ReturnToPool, DestroyPooledObject, 20, 40);
    }

    private void OnEnable () => _inputAction.Enable();
    private void OnDisable () => _inputAction.Disable();
    private void OnDestroy () => _inputAction.Dispose();
    private void OnShoot()
    {
        if (Time.time < _lastFireTime + _delayTime)
            return;

        _lastFireTime = Time.time;

        _impulse.GenerateImpulse(_recoilForce);

        _audioSource.resource = _clip;
        _audioSource.Play();

        var bullet = Instantiate(_prefab, _shootPoint.position, _shootPoint.rotation);
        bullet.AddRelativeForce(Vector3.forward * _bulletForce, ForceMode.Impulse);
    }

    private Rigidbody CreateBullet() => Instantiate(_prefab, _shootPoint.position, _shootPoint.rotation);
    private void ReturnToPool (Rigidbody bullet) => bullet.gameObject.SetActive(false);
    private void GetFromPool(Rigidbody bullet)
    {
        var obj = bullet.gameObject;
        obj.SetActive(true);
        obj.transform.SetPositionAndRotation(_shootPoint.position, _shootPoint.rotation);
    }
    private void DestroyPooledObject(Rigidbody bullet) => Destroy(bullet.gameObject);
}