using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private Rigidbody _rigidbody;
    public event Action<Bullet> OnCollide;

    public void AddDamageModifier (int amount) => _damage = Mathf.Max(_damage + amount, 0);

    public void Freeze()
    {
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        ResetVelocity();
        _rigidbody.isKinematic = true;
    }

    public void UnFreeze ()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        ResetVelocity();
    }

    private void ResetVelocity()
    {
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.velocity = Vector3.zero;
    }

    public void Shoot (Vector3 shootForce) => _rigidbody.AddRelativeForce(shootForce, ForceMode.Impulse);

    private void OnCollisionEnter (Collision collision)
    {
        if (collision.collider.TryGetComponent(out DamageTaker damager))
            damager.Damage(_damage);

        OnCollide?.Invoke(this);
    }
}
