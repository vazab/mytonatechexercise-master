using System;
using System.Collections;
using MyProject.Events;
using UnityEngine;

public class Mob : MonoBehaviour
{
	public float Damage = 1;
	public float MoveSpeed = 3.5f;
	public float Health = 3;
	public float MaxHealth = 3;

	public Action<float> OnHPChange = null;

	private void Start()
	{
		OnHPChange?.Invoke(Health);
	}

	public void TakeDamage(float amount)
	{
		if (Health <= 0)
		{
			return;
		}
		
		Health -= amount;
		OnHPChange?.Invoke(Health);
		
		if (Health <= 0)
		{
			Death();
		}
	}

	public void Heal(float amount)
	{
		if (Health <= 0)
			return;
		Health += amount;
		if (Health > MaxHealth)
		{
			Health = MaxHealth;
		}

		OnHPChange?.Invoke(Health);
	}

	public void Death()
	{
		EventBus.Pub(EventBus.MOB_KILLED);
		var components = GetComponents<IMobComponent>();
		
		foreach (var component in components)
		{
			component.OnDeath();
		}

		GetComponent<Collider>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		StartCoroutine(DestroyWithDelay());
	}

	private IEnumerator DestroyWithDelay()
	{
		yield return new WaitForSeconds(2f);

		Destroy(gameObject);
	}
}