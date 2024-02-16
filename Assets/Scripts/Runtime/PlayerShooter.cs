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
    private ObjectPool<Bullet> _bulletPool;

    [Header("VFX")]
    [SerializeField] private CinemachineImpulseSource _impulse;
    [SerializeField] private float _recoilForce;
    [SerializeField] private Bullet _prefab;
    [SerializeField] private float _bulletForce;

    [Header("SFX")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioResource _clip;

    private int _damageModifier;

    private void Awake ()
    {
        _inputAction.performed += (_) => OnShoot();
        _bulletPool = new(CreateBullet, GetFromPool, ReturnToPool, DestroyPooledObject, 20, 60);
        Item.OnShotSpeedChange += OnShotSpeedChange;
        Item.OnShotDamageChange += OnShotDamageChange;
    }
    private void OnShotSpeedChange(float speedChange) => _delayTime = Mathf.Max(_delayTime + speedChange, 0.1f);
    private void OnShotDamageChange(int dmg) => _damageModifier = Mathf.Max(_damageModifier + dmg, 5);
    private void OnEnable () => _inputAction.Enable();
    private void OnDisable () => _inputAction.Disable();
    private void OnDestroy ()
    {
        _inputAction.Dispose();
        Item.OnShotDamageChange -= OnShotDamageChange;
        Item.OnShotSpeedChange -= OnShotSpeedChange;
    }

    private void OnShoot()
    {
        if (Time.time < _lastFireTime + _delayTime)
            return;

        _lastFireTime = Time.time;

        _impulse.GenerateImpulse(_recoilForce);

        _audioSource.resource = _clip;
        _audioSource.Play();

        var bullet = _bulletPool.FromPool();
        bullet.SetDamageModifier(_damageModifier);
        bullet.Shoot(Vector3.forward * _bulletForce);
    }

    private Bullet CreateBullet ()
    {
        var bullet = Instantiate(_prefab, _shootPoint.position, _shootPoint.rotation);
        bullet.OnCollide += ReturnBulletToPool;
        
        ReturnToPool(bullet);
        
        return bullet;
    }
    private void ReturnBulletToPool (Bullet bullet) => _bulletPool.ToPool(bullet);
    private void ReturnToPool (Bullet bullet)
    {
        bullet.Freeze();
        bullet.gameObject.SetActive(false);
    }

    private void GetFromPool(Bullet bullet)
    {
        var obj = bullet.gameObject;
        obj.transform.SetPositionAndRotation(_shootPoint.position, _shootPoint.rotation);
        obj.SetActive(true);
        bullet.UnFreeze();
    }
    private void DestroyPooledObject(Bullet bullet) => Destroy(bullet.gameObject);

    public void OnPause (bool isPaused) => enabled = !isPaused;
}
