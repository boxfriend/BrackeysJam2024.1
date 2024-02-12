using UnityEngine;
using UnityEngine.Events;

public class DamageTaker : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private UnityEvent<int> _onDamage;
    [SerializeField] private UnityEvent _onDeath;
    public int Health { get; private set; }

    private void Awake () => Health = _maxHealth;

    public void Damage(int damage)
    {
        Health -= damage;
        
        _onDamage?.Invoke(damage);

        if (Health <= 0)
        {
            _onDeath?.Invoke();
        }
    }
}
