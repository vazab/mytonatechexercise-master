using UnityEngine;

public class WeaponPowerUp : MonoBehaviour
{
	[SerializeField] private int _type;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			other.GetComponent<Player>().ChangeWeapon(_type);
			
			Destroy(gameObject);
		}
	}
}