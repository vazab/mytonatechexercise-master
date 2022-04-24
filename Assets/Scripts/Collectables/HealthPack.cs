using UnityEngine;

public class HealthPack : MonoBehaviour
{
	[SerializeField] private int _health;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			other.GetComponent<Player>().Heal(_health);
			
			Destroy(gameObject);
		}
	}
}