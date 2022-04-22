﻿using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	public int Health;
	public int Damage;
	public float MoveSpeed;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			other.GetComponent<Player>().Upgrade(Health, Damage, MoveSpeed);
			Destroy(gameObject);
		}
	}
}