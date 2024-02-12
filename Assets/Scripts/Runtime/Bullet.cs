using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;

    public void AddDamageModifier (int amount) => _damage = Mathf.Max(_damage + amount, 0);

    private void OnCollisionEnter (Collision collision)
    {
        Debug.Log(collision.gameObject, collision.gameObject);
        if (collision.collider.TryGetComponent(out DamageTaker damager))
            damager.Damage(_damage);

        Destroy(gameObject);
    }
}
