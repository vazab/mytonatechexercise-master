using System;
using System.Collections.Generic;
using MyProject.Events;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health = 3f;
    [SerializeField] private float _maxHealth = 3f;
    [SerializeField] private float _moveSpeed = 3.5f;

    public static Player Instance; // razobratsia
    
	public Action<int> WeaponChanged = null;
    public Action<float> HealthChanged = null;
    public Action Upgraded = null;
	
    private float _damageMultiplier = 1f;

    public float MaxHealth => _maxHealth;
    public float DamageMultiplier => _damageMultiplier;
    public float MoveSpeed => _moveSpeed;
	public int CurrentWeapon { get; private set; }

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

		HealthChanged?.Invoke(_health);
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


    public void Upgrade(float hp, float dmgMuliplier, float ms)
    {
        _health += hp;
        _maxHealth += hp;
        _damageMultiplier += dmgMuliplier;
        _moveSpeed += ms;

        Upgraded?.Invoke();
        HealthChanged?.Invoke(_health);
    }

    public void ChangeWeapon(int type)
    {
		CurrentWeapon = type;
        WeaponChanged?.Invoke(type);
    }
}