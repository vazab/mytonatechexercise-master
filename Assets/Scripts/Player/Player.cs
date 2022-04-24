using System;
using MyProject.Events;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health = 3f;
    [SerializeField] private float _maxHealth = 3f;
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _moveSpeed = 3.5f;

    public static Player Instance;

    public Action<int> WeaponChanged = null;
    public Action<float> HealthChanged = null;
    public Action Upgraded = null;

    public float MaxHealth => _maxHealth;
    public float Damage => _damage;
    public float MoveSpeed => _moveSpeed;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void TakeDamage(float amount)
    {
        if (_health <= 0)
        {
            return;
        }
        
		_health -= amount;

        if (_health <= 0)
        {
            EventBus.Pub(EventBus.PLAYER_DEATH);
        }

        HealthChanged?.Invoke(_health);
    }

    public void Heal(float amount)
    {
        if (_health <= 0)
        {
            return;
        }

        _health += amount;

        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }

        HealthChanged?.Invoke(_health);
    }


    public void Upgrade(float hp, float dmg, float ms)
    {
        _damage += dmg;
        _health += hp;
        _maxHealth += hp;
        _moveSpeed += ms;

        Upgraded?.Invoke();
        HealthChanged?.Invoke(_health);
    }

    public void ChangeWeapon(int type)
    {
        WeaponChanged?.Invoke(type);
    }
}