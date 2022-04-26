using UnityEngine;

public class PowerUp : MonoBehaviour
{
	[SerializeField] private int _health;
	[SerializeField] private float _damageMultiplier;
	[SerializeField] private float _moveSpeed;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			other.GetComponent<Player>().Upgrade(_health, _damageMultiplier, _moveSpeed);
			
			Destroy(gameObject);
		}
	}
}