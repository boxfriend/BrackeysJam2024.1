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
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = true;
    }

    public void UnFreeze () => _rigidbody.isKinematic = false;
    public void Shoot (Vector3 shootForce) => _rigidbody.AddRelativeForce(shootForce, ForceMode.Impulse);

    private void OnCollisionEnter (Collision collision)
    {
        Debug.Log(collision.gameObject, collision.gameObject);
        if (collision.collider.TryGetComponent(out DamageTaker damager))
            damager.Damage(_damage);

        OnCollide?.Invoke(this);
    }
}
